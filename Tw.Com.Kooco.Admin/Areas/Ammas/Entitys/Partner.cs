using System;
using System.Collections.Specialized;
using System.Data;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys
{
    public class Partner
    {
        public int PartnerId { get; set; }
        public string ImgPath { get; set; }


        public void Fill(StringDictionary row)
        {
            if (row.Count <= 0)
            {
                PartnerId = 0;
                ImgPath = string.Empty;
            }
            else {
                PartnerId = Convert.ToInt32(row["PartnerId"]);
                ImgPath = row["ImgPath"];
               
            }
        }

        public void Fill(DataRow row)
        {
            PartnerId = Convert.ToInt32(row["PartnerId"]);
            ImgPath = row["ImgPath"].ToString();
        }
    }
}
