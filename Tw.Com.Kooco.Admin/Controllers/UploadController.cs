using jIAnSoft.Framework.Configuration;
using jIAnSoft.Framework.Security.Cryptography;
using jIAnSoft.Framework.Utility;
using System.IO;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Misc;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class UploadController : Controller
    {
        [Auth(Name = "上傳檔案", Description = "上傳檔案")]
        [HttpPost]
        public ActionResult UploadSingleFile(string HeadPath)
        {
            JsonNetResult result = new JsonNetResult();

            var httpRequest = HttpContext.Request;
            var postedFile = httpRequest.Files["file"];

            var remoteAttachDir = string.Format("{0}/{1}", HeadPath, System.DateTime.Now.ToString("yyyy/MM/dd"));

            var savePath = HttpContext.Server.MapPath(string.Format("~{0}", remoteAttachDir));
            Io.CreateFolder(savePath);

            var extension = Io.GetFileExt(postedFile.FileName);
            var filename = string.Format(
                "{0}.{1}",
                Md5.Encrypt(System.DateTime.Now.Ticks.ToString(Section.Get.Common.Culture)),
                extension);
            var filePath = Path.Combine(savePath, filename);

            if (extension.Equals("png") || extension.Equals("jpg") || extension.Equals("jpeg"))
            {
                TinyPNG.Instance.Upload(postedFile, filePath);
            }
            else
            {
                postedFile.SaveAs(filePath);
            }

            result.Data = new { path = remoteAttachDir, name = filename };
            return result;
        }

        [Auth(Name = "刪除檔案", Description = "刪除檔案")]
        public ActionResult DeleteSingleFile(string FilePath)
        {
            JsonNetResult result = new JsonNetResult();
            if (System.IO.File.Exists(HttpContext.Server.MapPath(string.Format("~{0}", FilePath))))
            {
                Io.DeleteFile(HttpContext.Server.MapPath(string.Format("~{0}", FilePath)));
            }
            result.Data = new { Ststus = "OK" };
            return result;
        }
    }
}