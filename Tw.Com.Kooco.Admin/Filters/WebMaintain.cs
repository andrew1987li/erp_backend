using jIAnSoft.Framework.Configuration;
using log4net;
using System;
using System.Web.Mvc;

namespace Tw.Com.Kooco.Admin.Filters
{
    public class WebMaintain : AuthorizeAttribute
    {
        private static ILog log = LogManager.GetLogger(typeof(WebMaintain));

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var Maintain = Section.Get.WebMaintain;
            if (Maintain.IsOpen)
            {
                //網站開放
                return;
            }
            else
            {
                if (DateTime.Now >= Maintain.StartTime && DateTime.Now <= Maintain.EndTime)
                {
                    string clientIp = filterContext.HttpContext.Request.UserHostAddress;
                    if (Maintain.AccessIp.Contains(clientIp))
                    {
                        //絕對能訪問的IP，令其通過
                        return;
                    }
                    else
                    {
                        //重導至停機維護頁面
                        if (string.IsNullOrEmpty(Maintain.RedirectUrl))
                        {
                            filterContext.Result = new HttpNotFoundResult();
                            return;
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult(Maintain.RedirectUrl);
                            return;
                        }
                    }
                }
                else
                {
                    //超過區間，自動開放
                    return;
                }
            }
        }
    }
}