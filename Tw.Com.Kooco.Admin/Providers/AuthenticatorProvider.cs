using jIAnSoft.Framework.Configuration;
using jIAnSoft.Framework.Security.Cryptography;
using jIAnSoft.Framework.Security.Principal;
using jIAnSoft.Framework.Web;
using jIAnSoft.Framework.Web.Security;
using log4net;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Tw.Com.Kooco.Admin.Entitys;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Providers.Authenticator;
using ManagerIdentity = Tw.Com.Kooco.Admin.Providers.Authenticator.ManagerIdentity;
using ManagerPrincipal = Tw.Com.Kooco.Admin.Providers.Authenticator.ManagerPrincipal;

namespace Tw.Com.Kooco.Admin.Providers
{
    public class AuthenticatorProvider : ManagerAuthenticatorBase
    {
        private static ILog log = LogManager.GetLogger(typeof(AuthenticatorProvider));

        private const string CookieSplittor = "ˊ_>ˋ";

        /// <summary>
        /// 將通過帳號密碼驗證的帳號寫入Cookie
        /// </summary>
        /// <param name="currentUser"></param>
        protected override void AddCookie(ManagerCurrentUser currentUser)
        {
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie(
                    HttpContext.Current.Request.Url.Authority,
                    FormsAuthentication.Encrypt(
                        new FormsAuthenticationTicket(
                            1,
                            currentUser.Account,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(Section.Get.Web.CookieTimeout),
                            true,
                            Utility.RawUrlEncode(
                                AzDG.Encrypt(
                                    string.Format(
                                        "{1}{0}{2}{0}{3}{0}{4}",
                                        CookieSplittor,
                                        currentUser.IdentityKey,
                                        currentUser.Account,
                                        currentUser.Name,
                                        currentUser.Nick)))
                            )))
                {
                    Domain = HttpContext.Current.Request.Url.Authority,
                    Expires = DateTime.MinValue
                    //Expires = DateTime.Now.AddSeconds(Section.Get.Web.CookieTimeout)
                });
        }

        private static void AddCookie(IPrincipal managerPrincipal)
        {
            var currentUser = ((ManagerIdentity)managerPrincipal.Identity).CurrentUser;
            Cookie.Set(
                new HttpCookie(
                    HttpContext.Current.Request.Url.Authority,
                    HttpUtility.UrlEncode(AzDG.Encrypt(
                        string.Format(
                            "{1}{0}{2}",
                            CookieSplittor,
                            currentUser.IdentityKey,
                            currentUser.Account
                            ))))
                {
                    Domain = Section.Get.Web.Domain,
                    //Expires = DateTime.MinValue
                    Expires = DateTime.Now.AddSeconds(Section.Get.Web.CookieTimeout)
                });
        }

        /// <summary>
        /// 從Cookie 中取得使用者資料
        /// </summary>
        /// <returns></returns>
        public override ManagerCurrentUser GetCookie()
        {
            if (!Cookie.IsExist(HttpContext.Current.Request.Url.Authority))
            {
                return null;
            }
            try
            {
                // var ticket = FormsAuthentication.Decrypt(Cookie.Get(DbName.AgmOfficial));
                var infoString = AzDG.Decrypt(Cookie.Get(HttpContext.Current.Request.Url.Authority));
                var info = infoString.Split(new[] { CookieSplittor }, StringSplitOptions.None);
                //20131203 加入快取機制
                var principal = CacheProvider.Get<IPrincipal>(info[1]);
                return principal != null
                    ? ((ManagerIdentity)(principal).Identity).CurrentUser
                    : User.FetchAdminUserDetail(info[1]);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 從 Cookie 或快取中取得使用者資料
        /// </summary>
        /// <returns></returns>
        public static IPrincipal GetUser()
        {
            if (!Cookie.IsExist(HttpContext.Current.Request.Url.Authority))
            {
                return null;
            }

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //return CacheProvider.Get<IPrincipal>(HttpContext.Current.User.Identity.Name);
                //重設過期時間
                AddCookie(HttpContext.Current.User);
                CacheProvider.Insert(HttpContext.Current.User.Identity.Name, HttpContext.Current.User);
                return HttpContext.Current.User;
            }
            try
            {
                var infoString = AzDG.Decrypt(Cookie.Get(HttpContext.Current.Request.Url.Authority));
                var info = infoString.Split(new[] { CookieSplittor }, StringSplitOptions.None);
                //20131203 加入快取機制
                var principal = CacheProvider.Get<IPrincipal>(info[1]);
                if (principal != null)
                {
                    AddCookie(principal);
                    CacheProvider.Insert(principal.Identity.Name, principal);
                    return principal;
                }
                principal = new ManagerPrincipal(info[1]);
                //重設過期時間
                AddCookie(principal);
                CacheProvider.Insert(info[1], principal);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 查詢資料庫內是否有使用者的資料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected override ManagerCurrentUser GetUser(string id, string password)
        {
            return ((ManagerIdentity)GetUser().Identity).CurrentUser;
            //return ((ManagerIdentity)GetUser().Identity).CurrentUser;
            //return ((ManagerIdentity)((Authenticator.ManagerPrincipal)GetUser()).Identity).CurrentUser;
        }

        protected override bool IsExistCookie()
        {
            return Cookie.IsExist(HttpContext.Current.Request.Url.Authority);
        }

        protected override void RemoveCookie()
        {
            Cookie.Delete(HttpContext.Current.Request.Url.Authority);
        }

        /// <summary>
        /// 帳號是否具有最高管理權限
        /// </summary>
        /// <param name="argStrAccount">帳號</param>
        /// <returns></returns>
        public static bool IsAdministrator(string argStrAccount)
        {
            return Section.Get.Web.MasterAdmin.Split('|').Any(t => argStrAccount == t);
        }

        /// <summary>
        /// 驗證來連線來源IP是否為管理者
        /// </summary>
        /// <param name="strConnectSourceIp"></param>
        /// <returns></returns>
        private bool VerifyConnectSourceIp(string strConnectSourceIp)
        {
            return Section.Get.Web.MasterAdminIp.Split(new[] { '|' }).Any(t => strConnectSourceIp == t);
        }

        /// <summary>
        /// 驗證登入的使用者是否有權限使用該功能
        /// </summary>
        /// <param name="argIntMenuCatgoryId"></param>
        /// <returns></returns>
        public bool VerifyFunctionPermission(int argIntMenuCatgoryId)
        {
            var user = ((ManagerIdentity)GetUser().Identity).CurrentUser;
            return (user.IsAdministrator ||
                    user.Functions.IndexOf($"[{argIntMenuCatgoryId}]",
                        StringComparison.Ordinal) != -1);
        }

        /// <summary>
        /// 登入驗證，若成功後，將登入資訊存入Cookie
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password">未加密過的密碼</param>
        /// <returns></returns>
        public override bool Login(string account, string password)
        {
            //取得帳號資料
            var userDetail = User.FetchAdminUserDetail(account);
            var encryptPassword = Md5.Encrypt(password);
            //檢查使用者的密碼
            if (string.IsNullOrEmpty(userDetail.EncryptPassword) || !userDetail.EncryptPassword.Equals(encryptPassword))
                return false;

            var principal = new ManagerPrincipal(userDetail.Account);
            //HttpContext.Current.User = principal;
            //Identity = HttpContext.Current.User.Identity as ManagerIdentity;
            //註冊Cookies
            //AddCookie(userDetail);
            AddCookie(principal);
            CacheProvider.Insert(userDetail.Account, principal);

            //System.Diagnostics.Debug.WriteLine(CurrentUser);
            //更新最後登入時間及IP
            ////userDetail.CurrentLoginIp = Request.ServerVariables["REMOTE_ADDR"];
            //dataProvider.UpdateAdminUserLastLogin(userDetail);
            return true;
        }

        public bool Login(UserParameter param)
        {
            //取得帳號資料
            var userDetail = User.FetchAdminUserDetail(param.User.Account);

            if (userDetail.Status == 2)
            {
                return false;
            }

            var encryptPassword = Md5.Encrypt(param.User.Password);
            //檢查使用者的密碼
            if (string.IsNullOrEmpty(userDetail.EncryptPassword) || !userDetail.EncryptPassword.Equals(encryptPassword))
                return false;
            //如果是最高管理者需要再驗證IP
            if (userDetail.IsAdministrator)
            {
                if (!VerifyConnectSourceIp(param.RemoteIp))
                {
                    return false;
                }
            }
            var principal = new ManagerPrincipal(userDetail.Account);
            //HttpContext.Current.User = principal;
            //Identity = HttpContext.Current.User.Identity as ManagerIdentity;
            //註冊Cookies
            //AddCookie(userDetail);
            AddCookie(principal);
            CacheProvider.Insert(userDetail.Account, principal);
            return true;
        }

        public new static void Logout() {
            CacheProvider.Clear(HttpContext.Current.User.Identity.Name);
            //var cookiesCount = HttpContext.Current.Request.Cookies.Count;
            //var cookies = new string[cookiesCount];
            //for (var i = 0; i < cookiesCount; i++) {
            //    Cookie.Set(
            //        new HttpCookie(HttpContext.Current.Request.Cookies[0].Name, "") {
            //            Domain = Section.Get.Web.Domain,
            //            //Expires = DateTime.MinValue
            //            Expires = DateTime.Now.AddSeconds(-Section.Get.Web.CookieTimeout)
            //        });
            //    // cookies[i] = HttpContext.Current.Request.Cookies[0].Name;
            //    // var cookie = new HttpCookie(cookies[i]);
            //    //// HttpCookie myCookie =
            //    // //if (null == cookie) return;
            //    // HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            //    // cookie.Value = string.Empty;
            //    // cookie.Domain = Section.Get.Web.Domain;
            //    // cookie.Expires = DateTime.UtcNow.AddDays(-2d);
            //    // HttpContext.Current.Response.Cookies.Add(cookie);

            //    // Cookie.Set(
            //    //new HttpCookie(HttpContext.Current.Request.Url.Authority,"")
            //    //{
            //    //    Domain = Section.Get.Web.Domain,
            //    //     //Expires = DateTime.MinValue
            //    //     Expires = DateTime.Now.AddSeconds(-Section.Get.Web.CookieTimeout)
            //    //});
            //}
            //Cookie.Set(
            //    new HttpCookie(HttpContext.Current.Request.Url.Authority, "") {
            //        Domain = Section.Get.Web.Domain,
            //        //Expires = DateTime.MinValue
            //        Expires = DateTime.Now.AddSeconds(-Section.Get.Web.CookieTimeout)
            //    });

            //FormsAuthentication.SignOut();
            Cookie.Clear();
        }

        /// <summary>
        /// 驗證使用者是否已登入，True =已登入
        /// </summary>
        /// <returns></returns>
        public static bool Logged()
        {
            return (HttpContext.Current.User != null) && HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}