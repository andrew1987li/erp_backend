using System;
using System.Collections.Specialized;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys {

    public class Announcement {
        private string _subTitle;

        private string _textBody;

        private string _awards;

        private string _imgPath;

        private string _link;

        public int AnnouncementId { get; set; }

        public bool IsTop { get; set; }

        public string Title { get; set; }
        
        public string TextBody {
            get { return _textBody ?? (_textBody = string.Empty); }
            set { _textBody = value; }
        }
        
        public int Sort { get; set; }
        public byte Type { get; set; }

        public string ImgPath {
            get { return _imgPath ?? (_imgPath = string.Empty); }
            set { _imgPath = value; }
        }

        public string Link {
            get { return _link ?? (_link = string.Empty); }
            set { _link = value; }
        }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public byte Status { get; set; }

        public void Fill(StringDictionary data) {
            AnnouncementId = Convert.ToInt32(data["AnnouncementId"]);
            IsTop = bool.Parse(data["IsTop"]);
            Title = data["Title"];
            TextBody = data["TextBody"];
            Link = data["Link"];
            ImgPath = data["ImgPath"];
            Sort = Convert.ToInt32(data["Sort"]);
            Type = Convert.ToByte(data["Type"]);
            Status = Convert.ToByte(data["Status"]);
            StartDate = DateTime.Parse(data["StartDate"]);
            EndDate = DateTime.Parse(data["EndDate"]);
            CreateTime = DateTime.Parse(data["CreateTime"]);
            UpdateTime = DateTime.Parse(data["UpdateTime"]);
        }
    }
}
