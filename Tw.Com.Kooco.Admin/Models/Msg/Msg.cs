using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class Msg
    {
        public string phonenumber { get; set; }
        public string content { get; set; }

        public Msg()
        {
            phonenumber = "";
            content = "";
        }
    }
}