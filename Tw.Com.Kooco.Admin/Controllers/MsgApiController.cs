using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Twilio;
using Tw.Com.Kooco.Admin.Models.Msg;
using System.Text.RegularExpressions;

namespace Tw.Com.Kooco.Admin.Controllers
{
  
    public class MsgApiController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public List<Results> send(string phonenumber, string content)
        {
            MsgFunction msgFunction = new MsgFunction();
            Msg msg = new Msg();
            msg.phonenumber = phonenumber;
            msg.content = content;

            List<Results> getres = msgFunction.sendSms(msg);

            return getres;
        }

        [HttpGet]
        [HttpPost]
        public List<Reports> Report(string type = "year", string year = "2016", string month = "1", string day = "1")
        {
            GetReport getrep = new GetReport();

            RangeDate range = getrep.getRange();

            char[] split_char = new char[] { '/' };



            List<Reports> reports;

            if (type == "year")
            {
                reports = getrep.getMonthInfo(year);
            }
            else if (type == "month")
            {
                reports = getrep.getWeekInfo(year, month);
            }
            else //(type == "day")
            {
                reports = getrep.getDayInfo(year, month);
            }

            return reports;
        }

        [HttpGet]
        [HttpPost]
        
        public List<DetailReport> Detail(string year="2016", string month="1", string day="1")
        {
            List<DetailReport> dreports;

            GetReport getrep = new GetReport();

            dreports = getrep.getDayDetailInfo(year, month, day);
            return dreports;
        }

        [HttpGet]
        [HttpPost]
        public string updateInfo(string MessageSid, string MessageStatus)
        {
            MsgFunction msgfunc = new MsgFunction();
            if (msgfunc.updateSmsInfo(MessageSid, MessageStatus))
            {
                return "success";
            }
            else return "fail";
        }

    }
}
