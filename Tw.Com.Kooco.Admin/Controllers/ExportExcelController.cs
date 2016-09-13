using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Models;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class ExportExcelController : Controller
    {
        [Auth(IsDefault = true)]
        public ActionResult Index(string FileName, IEnumerable table, Dictionary<string, string> columes)
        {
            var model = new ExportExcelModel();
            var ep = model.GetExcelPackage();
            model.SetWorksheets(ep, FileName, table, columes);

            var stream = new MemoryStream { Position = 0 };
            ep.SaveAs(stream);

            return File(
               stream.ToArray(),
               "application/octet-stream",
               string.Format("{0}-{1}.xlsx", FileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}