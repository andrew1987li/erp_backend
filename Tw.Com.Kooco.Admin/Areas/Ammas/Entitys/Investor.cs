using System;
using System.Collections.Specialized;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys
{
    public class Investor
    {
        public int InvestorId { get; set; }
        public string TypesOfCatagory { get; set; }
        public string Titel { get; set; }
        public string HyperLink { get; set; }
        public string DocPath { get; set; }
        private string _publishDate;

        public string PublishDate
        {
            get { return string.IsNullOrEmpty(_publishDate) ? "1900-01-01" : _publishDate; }
            set { _publishDate = value; }
        }

        public void Fill(StringDictionary data)
        {
            InvestorId = Convert.ToInt32(data["InvestorId"]);
            TypesOfCatagory = data["TypesOfCatagory"];
            Titel = data["Titel"];
            HyperLink = data["HyperLink"];
            DocPath = data["DocPath"];
            PublishDate = DateTime.Parse(data["PublishDate"]).ToString("yyyy-MM-dd");
        }
    }
}