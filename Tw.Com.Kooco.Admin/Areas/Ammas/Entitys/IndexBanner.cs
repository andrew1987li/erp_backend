using System;
using System.Collections.Specialized;
using System.Data;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys {
    public class IndexBanner {
        public int IndexBannerId { get; set; }
        public string ImgPath { get; set; }
        public string FirstString { get; set; }
        public string FirstStringColor { get; set; }
        public string SecondString { get; set; }
        public string SecondStringColor { get; set; }
        public string ThreeString { get; set; }
        public string ThreeStringColor { get; set; }
        public string Align { get; set; }
        public string Link { get; set; }
        public DateTime CreateTime { get; set; }

        public void Fill(StringDictionary row) {
            IndexBannerId = Convert.ToInt32(row["IndexBannerId"]);
            ImgPath = row["ImgPath"];
            FirstString = row["FirstString"];
            FirstStringColor = row["FirstStringColor"];
            SecondString = row["SecondString"];
            SecondStringColor = row["SecondStringColor"];
            ThreeString = row["ThreeString"];
            ThreeStringColor = row["ThreeStringColor"];
            Align = row["Align"];
            Link = row["Link"];
            CreateTime = DateTime.Parse(row["CreateTime"]);
        }

        public void Fill(DataRow row) {
            IndexBannerId = Convert.ToInt32(row["IndexBannerId"]);
            ImgPath = row["ImgPath"].ToString();
            FirstString = row["FirstString"].ToString();
            FirstStringColor = row["FirstStringColor"].ToString();
            SecondString = row["SecondString"].ToString();
            SecondStringColor = row["SecondStringColor"].ToString();
            ThreeString = row["ThreeString"].ToString();
            ThreeStringColor = row["ThreeStringColor"].ToString();
            Align = row["Align"].ToString();
            Link = row["Link"].ToString();
            CreateTime = DateTime.Parse(row["CreateTime"].ToString());
        }
    }
}