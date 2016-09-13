using log4net;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Models;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Models.Response;
using Tw.Com.Kooco.Admin.Providers;

namespace Tw.Com.Kooco.Admin.Controllers
{
    public class RoleController : Controller
    {
        private static ILog Log = LogManager.GetLogger(typeof(RoleController));

        #region -- 角色權限列表 --

        [Auth(Name = "角色權限", Description = "列表")]
        public ActionResult Index()
        {
            return View(
                new InternalDataTransferToView
                {
                    List = DataAccessProvider.Role.GetRoleList()
                });
        }

        #endregion -- 角色權限列表 --

        #region -- 角色編輯 --

        //[MvcSiteMapNode(Title = "角色編輯", ParentKey = "lv2_93", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"" }")]
        [Auth(Name = "角色編輯", Description = "新增與編輯共用，顯示角色細項")]
        public ActionResult Edit(RoleParameter param)
        {
            if (param.ID > 0)
            {
                return View(new InternalDataTransferToView
                {
                    Data = DataAccessProvider.Role.GetRoleDetail(param)
                });
            }
            else
            {
                return View(new InternalDataTransferToView
                {
                    Data = DataAccessProvider.Role.GetActions()
                });
            }
        }

        [Auth(Name = "編輯角色", Description = "新增與編輯共用，設定名稱與權限對照關係", Type = ResponseType.JSON_TEXT)]
        public ActionResult AjaxEdit(RoleParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                r.Code = ((param.ID > 0)
                    ? DataAccessProvider.Role.Update(param)
                    : DataAccessProvider.Role.Insert(param)).ToString();

                if (param.ActionIDs == null || param.ActionIDs.Count == 0)
                {
                    DataAccessProvider.RoleAction.Clear(param.ID);
                }
                else
                {
                    List<string> tmp = new List<string>();
                    foreach (string ActionID in param.ActionIDs)
                    {
                        tmp.Add(string.Format("({0}, {1})", param.ID, ActionID));
                    }
                    DataAccessProvider.RoleAction.Update(param.ID, string.Join(",", tmp.ToArray()));
                }

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

        #endregion -- 角色編輯 --

        #region -- 刪除角色 --

        [Auth(Name = "刪除角色", Description = "刪除角色", Type = ResponseType.JSON_TEXT)]
        public ActionResult AjaxDel(RoleParameter param)
        {
            JsonNetResult result = new JsonNetResult();
            var r = new DetailResponse();
            try
            {
                r.Code = (DataAccessProvider.Role.Delete(param)).ToString();
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

        #endregion -- 刪除角色 --
    }
}