using System.Web.Mvc;

namespace Tw.Com.Kooco.Admin.Areas.Ammas {
    public class AmmasAreaRegistration : AreaRegistration {
        public override string AreaName => "Ammas";

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Ammas_default",
                "Ammas/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}