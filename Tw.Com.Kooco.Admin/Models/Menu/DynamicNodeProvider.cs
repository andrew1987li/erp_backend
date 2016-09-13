using System;
using MvcSiteMapProvider;
using System.Collections.Generic;
using System.Linq;
using Tw.Com.Kooco.Admin.Entitys;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Providers;

namespace Tw.Com.Kooco.Admin.Models.Menu {
    public class DynamicNodeProvider : DynamicNodeProviderBase {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodes) {
            var function =
                DataAccessProvider.Function.List(new FunctionParameter { Function = new Function { Owner = 1 } }).Tables
                    [1];
            var functionTree = FunctionModel.GenTree(function);
            var returnValue = new List<DynamicNode>();
            var enumerable = (functionTree as Dictionary<string, Function>) ??
                             functionTree.ToDictionary(data => data.Name);
            CreateList(returnValue, enumerable, 1, null);
            return returnValue;
        }

        private void CreateList(List<DynamicNode> returnValue, Dictionary<string, Function> list, int dep,
            Function parent) {
            if (returnValue == null) {
                throw new ArgumentNullException(nameof(returnValue));
            }
            string lv = $"lv{dep}";
            var lvParent = $"lv{dep - 1}";

            foreach (var item in list) {
                var sub = item.Value;

                var node = new DynamicNode { Title = sub.Name };
                // node.RouteValues.Add(lv, sub.FunctionId);
                node.Attributes.Add("icon", sub.Icon);
                //node.Roles.Add(string.Format("Role_{0}", sub.FunctionId));

                if (string.IsNullOrEmpty(sub.Target)) {
                    node.Clickable = false;
                }
                else {
                    node.Url = sub.Target;
                }
                //node.Key = lv + "_" + sub.Name;
                node.Key = $"{lv}_{sub.FunctionId}";
                //node.RouteValues.Add(lv_parent, parent.FunctionId);
                if (parent != null) {
                    //node.ParentKey = lv_parent + "_" + parent.Name;
                    node.ParentKey = $"{lvParent}_{parent.FunctionId}";
                }

                returnValue.Add(node);

                if (sub.Son.Count > 0) {
                    CreateList(returnValue, sub.Son, dep + 1, sub);
                }
            }
        }
    }
}