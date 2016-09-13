using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Tw.Com.Kooco.Admin.Entitys;
using Tw.Com.Kooco.Admin.Providers;
using Tw.Com.Kooco.Admin.Providers.Authenticator;

namespace Tw.Com.Kooco.Admin.Models
{
    public class FunctionModel
    {
        /// <summary>
        ///  將登入者擁有的功能排列成階層樹後放到快取內，若快取已產生則直接使用快取內的功能階層樹
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IEnumerable<Entitys.Function> GenTree(DataTable t)
        {
            var user = ((ManagerIdentity)AuthenticatorProvider.GetUser().Identity).CurrentUser;
            //    =
            //    CacheProvider.Get<IEnumerable<Entitys.Function>>($"functionTree{user.Account}");
            //if (functilonTree != null) return functilonTree;

            var functionItems = t.Rows.Cast<DataRow>()
                .ToDictionary(
                    row => row["Code"].ToString(),
                    row => new Entitys.Function
                    {
                        FunctionId = Convert.ToInt32(row["FunctionId"]),
                        Code = row["Code"].ToString(),
                        Parent = row["Parent"].ToString(),
                        Name = row["Name"].ToString(),
                        Icon = row["Icon"].ToString(),
                        Parameters = row["Parameters"].ToString(),
                        Target = row["Target"].ToString(),
                        Son = new Dictionary<string, Entitys.Function>()
                    });
            var removeItem = new List<string>();
            foreach (var item in functionItems)
            {
                if (!functionItems.ContainsKey(item.Value.Parent) ||
                    functionItems[item.Value.Parent].Son.ContainsKey(item.Value.Code))
                    continue;

                functionItems[item.Value.Parent].Son[item.Value.Code] = functionItems[item.Value.Code];
                removeItem.Add(item.Value.Code);
            }
            foreach (var key in removeItem)
            {
                functionItems.Remove(key);
            }

            IEnumerable<Function> functilonTree = functionItems.Values.ToList();

            //CacheProvider.Insert($"functionTree{user.Account}", functilonTree);

            return functilonTree;
        }

        public static List<object> GetAllControllers()
        {
            List<object> result = new List<object>();

            Assembly asm = Assembly.GetExecutingAssembly();

            var controllerTypes = from t in asm.GetExportedTypes()
                                  where typeof(IController).IsAssignableFrom(t)
                                  select t;

            foreach (var c in controllerTypes)
            {
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
                    .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)));

                List<string> Actions = new List<string>();

                foreach (var info in q)
                {
                    if (info.ReturnType == typeof(ActionResult))
                    {
                        Actions.Add(info.Name);
                    }
                }

                result.Add(new
                {
                    Area = Area,
                    Name = Controller,
                    Actions = Actions
                });
            }

            return result;
        }
    }
}