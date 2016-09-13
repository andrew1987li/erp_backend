using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Misc;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Auth(Name = "Dashboard", Description = "設為首頁")]
        public ActionResult Index()
        {
            return View();
        }
    }
}