using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class Results
    {
       public string phonenumber { get; set; }
        public string state { get; set; }
        public string sid { get; set; }

        public Results()
        {
            phonenumber = "";
            state = "";
            sid = "";
        }
    }
}