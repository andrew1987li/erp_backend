using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Models.Msg;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class MsgController : Controller
    {
        // GET: Msg
        [HttpGet]
        public ActionResult Send()
        {
            return View(new Msg());
        }

        [HttpPost]
        public ActionResult Send(Msg msg)
        {
            MsgFunction msgFunction = new MsgFunction();

            List<Results> getres = msgFunction.sendSms(msg);

            return View("Results",getres);
        }


        public ActionResult Report(string type = "year",string thisyear="2016",string month="1",string day="1")
        {
            GetReport getrep = new GetReport();
            
            RangeDate range = getrep.getRange();

            char[] split_char = new char[] { '/' };

            string[] param = range.enddate.Split(split_char);


            ViewBag.Year = thisyear;
            ViewBag.Month =month;
            ViewBag.Day = day;
            ViewBag.type = type;

            List<Reports> reports;

            if(type == "year")
            {
                reports = getrep.getMonthInfo(thisyear);
            }
            else if(type == "month"){
                reports = getrep.getWeekInfo(thisyear, month);
            }else if(type == "day"){
                reports = getrep.getDayInfo(thisyear,month);
            }
            else
            {
                List<DetailReport> dreports;

                dreports = getrep.getDayDetailInfo(thisyear, month,day);
                return View("Detail", dreports);
            }
            return View(reports);

            
        }


    }
}