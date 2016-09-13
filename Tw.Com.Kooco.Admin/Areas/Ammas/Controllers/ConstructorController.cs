using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Omu.AwesomeMvc; // for GridParams,
using Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters;
using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Controllers.Grid
{
    public class ConstructorController : Controller
    {
        // GET: Ammas/Constructor
        public ActionResult Index()
        {
            return View();
        }
    }

    public class MasterDetailCrudDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*public ActionResult Create()
        {
            // needed so we could add type even before the type detail is created/saved
            var rest = conCompanyParameter.ConstructorDb.Insert(new ConstructionType());

            return PartialView(new RestaurantInput { Id = rest.Id });
        }

        public ActionResult RestaurantGridGetItems(GridParams g)
        {
            var model = new GridModelBuilder<Restaurant>(Db.Restaurants.Where(o => o.IsCreated).AsQueryable(), g)
            {
                Key = "Id",
                GetItem = () => Db.Get<Restaurant>(Convert.ToInt32(g.Key))
            }.Build();
            return Json(model);
        }

        [HttpPost]
        public ActionResult Create(RestaurantInput input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(input);
            }

            var restaurant = Db.Get<Restaurant>(input.Id);
            restaurant.Name = input.Name;
            restaurant.IsCreated = true;

            return Json(restaurant); // use MapToGridModel like in Grid Crud Demo when grid uses Map
        }

        public ActionResult Edit(int id)
        {
            var rest = Db.Get<Restaurant>(id);
            return PartialView("Create", new RestaurantInput { Id = id, Name = rest.Name });
        }

        [HttpPost]
        public ActionResult Edit(RestaurantInput input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", input);
            }

            var rest = Db.Get<Restaurant>(Convert.ToInt32(input.Id));

            rest.Name = input.Name;

            return Json(new { rest.Id });
        }

        public ActionResult Delete(int id, string gridId)
        {
            var restaurant = Db.Get<Restaurant>(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                GridId = gridId,
                Message = string.Format("Are you sure you want to delete restaurant <b>{0}</b> ?", restaurant.Name)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            Db.Delete<Restaurant>(input.Id);
            return Json(new { input.Id });
        }*/
    }
}