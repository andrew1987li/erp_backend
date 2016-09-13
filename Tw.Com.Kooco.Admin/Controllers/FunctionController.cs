using jIAnSoft.Framework.Configuration;
using log4net;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Entitys;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Models;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Models.Response;
using Tw.Com.Kooco.Admin.Providers;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class FunctionController : Controller
    {
        private static ILog Log = LogManager.GetLogger(typeof(FunctionController));

        #region -- 選單列表 --

        [SiteMapCacheRelease]
        [Auth(Name = "選單", Description = "顯示所有選單，可在此頁拖拉設定排序")]
        public ActionResult Index(FunctionParameter param)
        {
            return View(
                new InternalDataTransferToView
                {
                    List = DataAccessProvider.Function.List(new FunctionParameter()),
                    Page = param.Page,
                    PageSize = param.PageSize,
                    KeyWord = param.KeyWord
                });
        }

        #endregion -- 選單列表 --

        #region -- 編輯選單 --

        //[MvcSiteMapNode(Title = "編輯", ParentKey = "lv2_2", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"" }")]
        [HttpPost]
        [Auth(Name = "編輯選單", Description = "新增與編輯共用，設定名稱、路徑")]
        public ActionResult Edit(FunctionParameter param)
        {
            if (param.Function.FunctionId > 0)
            {
                param.Function.Fill(DataAccessProvider.Function.Detail(param));
            }
            else
            {
                param.Function.Priority =
                    DataAccessProvider.Function.NextPriority(new FunctionParameter
                    {
                        Function = new Function { FunctionId = param.Function.ParentFunctionId }
                    });
            }
            param.Controllers = FunctionModel.GetAllControllers();
            return View(new InternalDataTransferToView { Data = param });
        }

        [HttpPost]
        public ActionResult AjaxDelete(FunctionParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                r.Code = DataAccessProvider.Function.Delete(param).ToString(Section.Get.Common.Culture);
                r.Ok = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }

            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxEdit(FunctionParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                if (DataAccessProvider.Function.Check(param))
                {
                    r.Code = ((param.Function.FunctionId > 0)
                        ? DataAccessProvider.Function.Update(param)
                        : DataAccessProvider.Function.Insert(param)).ToString(Section.Get.Common.Culture);
                    r.Ok = true;
                }
                else
                {
                    r.Code = "-2";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }

            result.Data = r;
            return result;
        }

        [HttpPost]
        public ActionResult AjaxSort(FunctionParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                JArray arr = JsonConvert.DeserializeObject<JArray>(param.json);

                foreach (JObject item in arr)
                {
                    long id = item.GetValue("id").ToObject<Int64>();
                    int priority = item.GetValue("priority").ToObject<Int32>();
                    string code = item.GetValue("code").ToObject<string>();
                    string parent = item.GetValue("parent").ToObject<string>();
                    int dep = item.GetValue("dep").ToObject<Int32>();

                    DataAccessProvider.Function.UpdatePriority(id, priority, code, parent, dep);
                }

                foreach (JObject item in arr)
                {
                    long id = item.GetValue("id").ToObject<Int64>();
                    DataAccessProvider.Function.UpdateSort(id);
                }

                r.Code = "OK";
                r.Ok = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                r.Code = "-11";
            }

            result.Data = r;
            return result;
        }

        #endregion -- 編輯選單 --
    }
}