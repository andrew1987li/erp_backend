using log4net;
using Microsoft.Ajax.Utilities;
using System.IO;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Models;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class MiscController : Controller
    {
        private static ILog Log = LogManager.GetLogger(typeof(MiscController));

        /// <summary>
        /// 壓縮Js 檔
        /// </summary>
        [AllowAnonymous]
        public void Minifier()
        {
            var minifier = new Minifier();
            ProcessDirectory(minifier, System.Web.HttpContext.Current.Server.MapPath(@"~/Scripts"));
        }

        [AllowAnonymous]
        public ActionResult Message(MiscModel.MessageParameter param)
        {
            return View(param);
        }

        private static void ProcessDirectory(Minifier minifier, string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.js");
            foreach (string fileName in fileEntries)
                ProcessFile(minifier, fileName);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(minifier, subdirectory);
        }

        private static void ProcessFile(Minifier minifier, string path)
        {
            if (path.EndsWith(".min.js"))
            {
                return;
            }

            //Debug.WriteLine(path);
            try
            {
                string str = minifier.MinifyJavaScript(System.IO.File.ReadAllText(path));
                string filename = path.Replace(".js", ".min.js");
                //Debug.WriteLine(filename);
                System.IO.File.WriteAllText(filename, str);
            }
            catch (IOException ex)
            {
                Log.Error(ex.Message, ex);
                //Console.WriteLine("File \"{0}\" was not found.", args[i]);
            }
        }
    }
}