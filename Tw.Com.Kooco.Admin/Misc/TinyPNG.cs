using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tw.Com.Kooco.Admin.Misc
{
    public class TinyPNG
    {
        private static ILog Log = LogManager.GetLogger(typeof(TinyPNG));

        private static TinyPNG _instance;

        public static TinyPNG Instance
        {
            get { return _instance ?? (_instance = new TinyPNG()); }
        }

        public byte[] ConvertToByteArray(HttpPostedFileBase file)
        {
            var binaryData = new byte[file.InputStream.Length];
            file.InputStream.Read(binaryData, 0, (int)file.InputStream.Length);
            file.InputStream.Seek(0, SeekOrigin.Begin);
            file.InputStream.Close();
            return binaryData;
        }

        public void Upload(HttpPostedFileBase file, string output)
        {
            string key = "GOVodT4WQYNXk90vqalM4Bb2C2Q4Rz-f";
            //測試用
            //key = "QBg44uKCODuIg8QWmRmrBq1suv_1RYBp";

            string url = "https://api.tinify.com/shrink";

            WebClient client = new WebClient();
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + key));
            client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + auth);

            var image = ConvertToByteArray(file);

            try
            {
                client.UploadData(url, image);
                /* Compression was successful, retrieve output from Location header. */

                client.DownloadFile(client.ResponseHeaders["Location"], output);
            }
            catch (WebException e)
            {
                /* Something went wrong! You can parse the JSON body for details. */
                Log.Error(e.Message, e);
            }
        }
    }
}