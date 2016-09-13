using jIAnSoft.Framework.Configuration;
using jIAnSoft.Framework.Security.Cryptography;
using log4net;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Models;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Models.Response;
using Tw.Com.Kooco.Admin.Providers;
using Tw.Com.Kooco.Admin.Providers.Authenticator;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class UserController : Controller
    {
        private static ILog Log = LogManager.GetLogger(typeof(UserController));

        #region -- 權限不足 --

        [AllowAnonymous]
        public ActionResult AccessDenied(int rt)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();

            switch ((ResponseType)rt)
            {
                default:
                case ResponseType.HTML:
                    return View();

                case ResponseType.JSON:
                    r.Ok = false;
                    r.Code = "-999";
                    r.Data = "Access Denied";
                    result.Data = r;
                    return result;

                case ResponseType.JSON_TEXT:
                    r.Ok = false;
                    r.Code = "-999";
                    r.Data = "Access Denied";
                    result.Data = r;
                    return result;
            }
        }

        #endregion -- 權限不足 --

        [Auth(Name = "取得系統時間", Description = "", IsDefault = true)]
        [HttpGet]
        public ActionResult ServerTime()
        {
            return Content(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        #region -- 帳號列表 --

        [Auth(Name = "帳號", Description = "顯示列表")]
        public ActionResult Index(UserParameter param)
        {
            return View(
                new InternalDataTransferToView
                {
                    List = DataAccessProvider.User.List(param),
                    Page = param.Page,
                    PageSize = param.PageSize,
                    KeyWord = param.KeyWord
                });
        }

        #endregion -- 帳號列表 --

        #region -- 編輯帳號 --

        [Auth(Name = "編輯帳號", Description = "新增與編輯共用，顯示帳號細項")]
        //[MvcSiteMapNode(Title = "編輯帳號", ParentKey = "lv2_62", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"" }")]
        [HttpPost]
        public ActionResult Edit(UserParameter param)
        {
            return View(new InternalDataTransferToView { Data = param });
        }

        [Auth(Name = "編輯帳號", Description = "顯示明細")]
        [HttpPost]
        public ActionResult AjaxDetail(UserParameter param)
        {
            param.User.Fill();

            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                r.Ok = !string.IsNullOrEmpty(param.User.Account);
                r.Data = param.User;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        [Auth(Name = "編輯帳號", Description = "設定帳號")]
        [HttpPost]
        public ActionResult AjaxUpdate(UserParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                string RoleIDs = param.User.RoleIDs;
                if (RoleIDs == null || RoleIDs.IndexOf(",") < 0 || RoleIDs.Equals(","))
                {
                    DataAccessProvider.UserRole.Clear(param.User.IdentityKey);
                }
                else
                {
                    var tmp = new List<string>();
                    foreach (var RoleID in RoleIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        tmp.Add($"({param.User.IdentityKey}, {RoleID})");
                    }
                    DataAccessProvider.UserRole.Update(param.User.IdentityKey, string.Join(",", tmp.ToArray()));
                }

                r.Code = DataAccessProvider.User.Update(param).ToString(Section.Get.Common.Culture);
                r.Ok = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        #endregion -- 編輯帳號 --

        #region -- 設定帳號特殊權限 --

        [Auth(Name = "設定帳號特殊權限", Description = "顯示清單")]
       // [MvcSiteMapNode(Title = "特殊權限設定", ParentKey = "lv2_62", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"" }")]
        public ActionResult Action(UserParameter param)
        {
            param.User.Fill();
            return View(new InternalDataTransferToView
            {
                Data = param,
                List = DataAccessProvider.User.GetUserAction(param.User.IdentityKey)
            });
        }

        [Auth(Name = "設定帳號特殊權限", Description = "設定")]
        public ActionResult AjaxAction(UserParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                if (param.ActionIDs == null || param.ActionIDs.Count == 0)
                {
                    r.Code = DataAccessProvider.UserAction.Clear(param.UserID).ToString();
                }
                else
                {
                    List<string> tmp = new List<string>();
                    foreach (string ActionID in param.ActionIDs)
                    {
                        tmp.Add(string.Format("({0}, {1})", param.UserID, ActionID));
                    }
                    r.Code = DataAccessProvider.UserAction.Update(param.UserID, string.Join(",", tmp.ToArray())).ToString();
                }

                r.Ok = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }

            result.Data = r;
            return result;
        }

        [Auth(Name = "設定帳號權限群組", Description = "設定")]
        [HttpPost]
        public ActionResult AjaxPermsEdit(UserParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                r.Code = DataAccessProvider.User.PermsUpdate(param).ToString(Section.Get.Common.Culture);
                r.Ok = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }
            result.Data = r;
            return result;
        }

        #endregion -- 設定帳號特殊權限 --

        #region -- 修改個人密碼 --

        [Auth(Name = "修改個人密碼", Description = "顯示", IsDefault = true)]
        public ActionResult ChangePassword(UserParameter param)
        {
            return View();
        }

        [Auth(Name = "修改個人密碼", Description = "設定", IsDefault = true)]
        public ActionResult AjaxChangePassword(string old, string pwd)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                old = Md5.Encrypt(old);
                string Password = AzDG.Encrypt(pwd);
                string EncryptPassword = Md5.Encrypt(pwd);

                Tw.Com.Kooco.Admin.Entitys.User userDetail = ((ManagerIdentity)AuthenticatorProvider.GetUser().Identity).CurrentUser;

                if (string.IsNullOrEmpty(userDetail.EncryptPassword) || !userDetail.EncryptPassword.Equals(old))
                {
                    r.Code = "-1";
                    r.Data = "舊密碼驗證錯誤";
                }
                else if (pwd.Length < 8)
                {
                    r.Code = "-1";
                    r.Data = "新密碼長度必須大於或等於8個字元";
                }
                else
                {
                    int n = DataAccessProvider.User.ChangePassword(userDetail.Account, Password, EncryptPassword);
                    if (n == 1)
                    {
                        userDetail.Password = Password;
                        userDetail.EncryptPassword = EncryptPassword;
                        r.Ok = true;
                    }
                    else
                    {
                        r.Code = "-2";
                        r.Data = "修改密碼失敗";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }

            result.Data = r;
            return result;
        }

        #endregion -- 修改個人密碼 --

        #region -- 註冊 --

        [AllowAnonymous]
        public ActionResult Register(UserParameter param)
        {
            //20131205 註冊改用Ajax處理
            if (AuthenticatorProvider.Logged())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new InternalDataTransferToView { Data = param });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AjaxRegister(UserParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = UserModel.Register(param);
            result.Data = r;
            return result;
        }

        #endregion -- 註冊 --

        #region -- 登入 --

        [AllowAnonymous]
        public ActionResult Login()
        {
            //20131205 登入改用Ajax處理
            if (AuthenticatorProvider.Logged())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new InternalDataTransferToView());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AjaxLogin(UserParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                if (Request.ServerVariables["HTTP_VIA"] != null)
                {
                    // 穿過代理服務器取遠程用戶真實IP地址
                    param.RemoteIp = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    param.RemoteIp = Request.ServerVariables["REMOTE_ADDR"].ToString();
                }

                r.Ok = new AuthenticatorProvider().Login(param);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }

            result.Data = r;
            return result;
        }

        #endregion -- 登入 --

        #region -- 登出 --

        [Auth(Name = "登出", Description = "預設已登入帳號可使用", IsDefault = true)]
        public ActionResult Logout()
        {
            UserModel.Logout();
            return RedirectToAction(
                "Message",
                "Misc",
                new RouteValueDictionary {
                    { "IsTransfer", "true" },
                    { "StayTime", "1" },
                    { "Target", "/" },
                    { "Message", HttpUtility.UrlEncode("Logout success") }
                });
        }

        #endregion -- 登出 --
    }
}