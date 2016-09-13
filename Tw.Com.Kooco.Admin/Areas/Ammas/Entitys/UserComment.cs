using System;
using System.Collections.Specialized;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys {
    public class UserComment {
        public int UserCommentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ImgPath { get; set; }

        public void Fill(StringDictionary row) {
            if (string.IsNullOrEmpty(row["UserCommentId"])) {
                return;
            }
            UserCommentId = Convert.ToInt32(row["UserCommentId"]);
            Name = row["Name"];
            Title = row["Title"];
            Comment = row["Comment"];
            ImgPath = row["ImgPath"];
        }
    }
}
