using System;
using System.Collections.Specialized;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys {
    public class Literature {
        public int LiteratureId { get; set; }
        public string TypesOfCancer { get; set; }
        public string LiteratureTitel { get; set; }
        public string HyperLink { get; set; }
        private string _publishDate;

        public string PublishDate {
            get { return string.IsNullOrEmpty(_publishDate) ? "1900-01-01" : _publishDate; }
            set { _publishDate = value; }
        }

        public void Fill(StringDictionary data) {
            LiteratureId = Convert.ToInt32(data["LiteratureId"]);
            TypesOfCancer = data["TypesOfCancer"];
            LiteratureTitel = data["LiteratureTitel"];
            HyperLink = data["HyperLink"];
            PublishDate = DateTime.Parse(data["PublishDate"]).ToString("yyyy-MM-dd");
        }
    }
}