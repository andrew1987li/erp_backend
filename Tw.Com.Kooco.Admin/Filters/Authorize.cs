using jIAnSoft.Framework.Configuration;
using log4net;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Tw.Com.Kooco.Admin.Entitys;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Providers;
using Tw.Com.Kooco.Admin.Providers.Authenticator;

namespace Tw.Com.Kooco.Admin.Filters
{
    public sealed class Authorize : AuthorizeAttribute
    {
        private static ILog log = LogManager.GetLogger(typeof(Authorize));

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.User = AuthenticatorProvider.GetUser();
            var aa = typeof(AllowAnonymousAttribute);
            var ad = filterContext.ActionDescriptor;
            var skipAuthorization = ad.GetCustomAttributes(aa, true).Any() || ad.ControllerDescriptor.IsDefined(aa, true);

            AuthAttribute Auth = null;
            if (ad.GetCustomAttributes(typeof(AuthAttribute), true).Any())
            {
                var list = ad.GetCustomAttributes(typeof(AuthAttribute), true);
                Auth = (AuthAttribute)list[0];
            }

            string clientIp = filterContext.HttpContext.Request.UserHostAddress;
            if (Auth != null && Auth.AllowIpList != null && Auth.AllowIpList.Contains(clientIp))
            {
                return;
            }

            if (!skipAuthorization)
            {
                base.OnAuthorization(filterContext);

                if (AuthenticatorProvider.GetUser() == null)
                {
                    return;
                }

                User user = ((ManagerIdentity)AuthenticatorProvider.GetUser().Identity).CurrentUser;

                var TokensForArea = filterContext.RouteData.DataTokens["area"];
                string area = (TokensForArea == null) ? null : TokensForArea.ToString();
                var controller = ad.ControllerDescriptor.ControllerName;
                var action = ad.ActionName;
                string path = (area == null) ? string.Format("/{0}/{1}", controller, action) : string.Format("/{0}/{1}/{2}", area, controller, action);

                //log.DebugFormat("{0} {1} {2}", Section.Get.Web.MasterAdmin, user.Account, Section.Get.Web.MasterAdmin.Contains(user.Account));

                if (Section.Get.Web.MasterAdmin.Contains(user.Account) && Section.Get.Web.MasterAdminIp.Contains(clientIp))
                {
                    return;
                }

                if (!user.AuthPath.Contains(path) && (Auth != null && !Auth.IsDefault))
                {
                    object obj;
                    if (Auth != null)
                    {
                        obj = new { area = "", controller = "User", action = "AccessDenied", rt = (int)Auth.Type };
                    }
                    else
                    {
                        obj = new { area = "", controller = "User", action = "AccessDenied", rt = (int)ResponseType.HTML };
                    }

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(obj));
                    return;
                }
            }
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return AuthenticatorProvider.Logged();
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        private object GetUser()
        {
            throw new System.NotImplementedException();
        }
    }
}