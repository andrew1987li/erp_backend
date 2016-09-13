using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tw.Com.Kooco.Admin.Models.Msg
{
    public class SmsUser
    {
        public string ID { get; set; }

        [Required]
        public string NAME { get; set; }
        [Required]
        public string EMAIL { get; set; }
        [Required]
        public string AUTH { get; set; }
        [Required]
        public string AUTHKEY { get; set; }
        [Required]
        public string STATE { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DATE { get; set; }

        public SmsUser()
        {
            ID = "";
            NAME = ""; EMAIL = ""; AUTH = ""; AUTHKEY = ""; STATE = ""; DATE = new DateTime();
        }
    }
}