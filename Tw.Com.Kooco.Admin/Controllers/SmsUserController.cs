using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Models.Msg;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class SmsUserController : Controller
    {
        // GET: SmsUser
        public ActionResult Index()
        {
            SmsUserContext smscon = new SmsUserContext();
            List<SmsUser> user_list = smscon.getUsers();
            return View(user_list);
        }


        // GET: SmsUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SmsUser/Create
        [HttpPost]
        public ActionResult Create(SmsUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SmsUserContext con = new SmsUserContext();
                    con.createUser(user);

                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here
                else
                {
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: SmsUser/Edit/5
        public ActionResult Edit(string id)
        {
            SmsUserContext con = new SmsUserContext();
            SmsUser user = con.getUser(id);
            return View(user);
        }

        // POST: SmsUser/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, SmsUser user)
        {
            SmsUserContext con = new SmsUserContext();
            SmsUser muser;
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    user.ID = id;
                    con.updateUser(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    muser = con.getUser(id);
                    return View(muser);
                }
            }
            catch
            {
                muser = con.getUser(id);
                return View(muser);
            }
        }

        // GET: SmsUser/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                SmsUserContext con = new SmsUserContext();
                con.deleteUser(id);
                
            }
            catch
            {
               
            }
            return RedirectToAction("Index");
        }
    }
}
