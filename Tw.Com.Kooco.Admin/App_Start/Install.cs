using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Misc;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Providers;

namespace Tw.Com.Kooco.Admin.App_Start
{
    public class Install
    {
        private static ILog log = LogManager.GetLogger(typeof(Install));

        public void Start()
        {
            DataTable actionTable = DataAccessProvider.Actions.GetActions();

            List<ActionParameter> all = GetSystemAllActions();
            List<ActionParameter> insert = new List<ActionParameter>();

            foreach (DataRow row in actionTable.Rows)
            {
                var query = from c in all
                            where c.Area == row["Area"].ToString()
                                && c.Controller == row["Controller"].ToString()
                                && c.Action == row["Action"].ToString()
                            select c;
                if (query.Count() == 0)
                {
                    DataAccessProvider.Actions.DeleteActions(Convert.ToInt64(row["ID"]));
                }
            }

            foreach (ActionParameter action in all)
            {
                var query = from c in actionTable.AsEnumerable()
                            where c.Field<string>("Area") == action.Area
                                && c.Field<string>("Controller") == action.Controller
                                && c.Field<string>("Action") == action.Action
                            select c;

                if (query.Count() > 0)
                {
                    DataRow row = query.First();
                    bool skip = action.Name.Equals(row["Name"].ToString())
                            && action.Description.Equals(row["Description"].ToString())
                            && action.Default == Convert.ToInt32(row["Default"])
                            && action.Type == Convert.ToInt32(row["Type"]);
                    if (skip) continue;

                    action.ID = Convert.ToInt64(row["ID"]);
                    action.Disable = Convert.ToInt32(row["Disable"]);
                    DataAccessProvider.Actions.UpdateActions(action);
                }
                else
                {
                    insert.Add(action);
                }
            }

            if (insert.Count > 0)
            {
                int result = DataAccessProvider.Actions.InsertActions(insert);
                log.Info("insert:" + result);
            }
        }

        private List<ActionParameter> GetSystemAllActions()
        {
            List<ActionParameter> result = new List<ActionParameter>();

            Assembly asm = Assembly.GetExecutingAssembly();

            var controllerTypes = from t in asm.GetExportedTypes()
                                  where typeof(IController).IsAssignableFrom(t)
                                  select t;

            foreach (var c in controllerTypes)
            {
                if (c.IsDefined(typeof(AllowAnonymousAttribute))) continue;
                var Controller = c.Name.Replace("Controller", "");

                var controllerFullName = c.ToString();
                var Area = string.Empty;
                int n = controllerFullName.IndexOf(".Areas.", System.StringComparison.Ordinal);
                if (n >= 0)
                {
                    n += ".Areas.".Length;
                    int len = controllerFullName.IndexOf(".", n, System.StringComparison.Ordinal) - n;
                    Area = controllerFullName.Substring(n, len);
                }

                var q = asm.GetTypes()
                    .Where(type => c.IsAssignableFrom(type))//filter controllers
                    .SelectMany(type => type.GetMethods())
                    .Where(method => method.IsPublic
                        && !method.IsDefined(typeof(NonActionAttribute))
                        && !method.IsDefined(typeof(AllowAnonymousAttribute)));

                foreach (var m in q)
                {
                    AuthAttribute Auth = (AuthAttribute)(m.GetCustomAttributes(false).FirstOrDefault(x => x is AuthAttribute));
                    //if (Auth == null) continue;
                    if (m.ReturnType != typeof(ActionResult)) continue;

                    var Action = m.Name;
                    var Name = (Auth == null) ? string.Empty : Auth.Name;
                    var Description = (Auth == null) ? string.Empty : Auth.Description;
                    var Default = (Auth == null) ? false : Auth.IsDefault;
                    int Type = (Auth == null) ? 0 : (int)Auth.Type;

                    result.Add(new ActionParameter()
                    {
                        Name = Name,
                        Description = Description,
                        Default = (Default) ? 1 : 0,
                        Area = Area,
                        Controller = Controller,
                        Action = Action,
                        Type = Type,
                        Disable = 0
                    });
                }
            }

            return result;
        }
    }
}