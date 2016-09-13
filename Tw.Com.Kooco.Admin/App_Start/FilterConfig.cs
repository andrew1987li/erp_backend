using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Filters;

namespace Tw.Com.Kooco.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new WebMaintain());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Authorize());
        }
    }
}