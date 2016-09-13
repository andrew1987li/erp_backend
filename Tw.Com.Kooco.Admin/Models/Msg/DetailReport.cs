using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class DetailReport
    {
        public int id { get; set; }
        public string phonenumber { get; set; }

        public string sid { get; set; }
        public string state { get; set; }
        public string date { get; set; }

        public string body { get; set; }
        public DetailReport()
        {
            id = 0; phonenumber = ""; sid = ""; state = ""; date = "";
        }
    }
}