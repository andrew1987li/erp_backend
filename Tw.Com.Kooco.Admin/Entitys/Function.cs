using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace Tw.Com.Kooco.Admin.Entitys
{
    public class Function
    {
        private static ILog log = LogManager.GetLogger(typeof(Function));
        private string _target;

        private string _area;

        private string _controller;

        private string _action;

        private string _parameters;

        private string _icon;

        public int FunctionId { get; set; }

        public int Priority { get; set; }

        public int Owner { get; set; }

        public int Depth { get; set; }

        public string Sort { get; set; }

        public string Target
        {
            get { return string.IsNullOrEmpty(_target) ? "" : _target; }
            set { _target = string.IsNullOrEmpty(value) ? "" : value; }
        }

        public string Area
        {
            get { return string.IsNullOrEmpty(_area) ? "" : _area; }
            set { _area = string.IsNullOrEmpty(value) ? "" : value; }
        }

        public string Controller
        {
            get { return string.IsNullOrEmpty(_controller) ? "" : _controller; }
            set { _controller = string.IsNullOrEmpty(value) ? "" : value; }
        }

        public string Action
        {
            get { return string.IsNullOrEmpty(_action) ? "" : _action; }
            set { _action = string.IsNullOrEmpty(value) ? "" : value; }
        }

        // public string Target { get; set; }
        public string Parameters
        {
            get { return string.IsNullOrEmpty(_parameters) ? "" : _parameters; }
            set { _parameters = string.IsNullOrEmpty(value) ? "" : value; }
        }

        // public string Parameters { get; set; }
        public string Parent { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Icon
        {
            get { return string.IsNullOrEmpty(_icon) ? "" : _icon; }
            set { _icon = string.IsNullOrEmpty(value) ? "" : value; }
        }

        public string ParentFunctionName { get; set; }

        public int ParentFunctionId { get; set; }

        public Dictionary<string, Function> Son { get; set; }

        public void Fill(StringDictionary data)
        {
            Priority = Convert.ToInt32(data["Priority"]);
            Owner = Convert.ToInt32(data["Owner"]);
            Depth = Convert.ToInt32(data["Depth"]);
            Sort = data["Sort"];
            Target = data["Target"];
            Area = data["Area"];
            Controller = data["Controller"];
            Action = data["Action"];
            Parameters = data["Parameters"];
            Parent = data["Parent"];
            Code = data["Code"];
            Name = data["Name"];
            Icon = data["Icon"];
            ParentFunctionName = data["ParentFunctionName"];
        }

        public void Fill(DataRow data)
        {
            Priority = Convert.ToInt32(data["Priority"]);

            Depth = Convert.ToInt32(data["Depth"]);

            Target = data["Target"].ToString();
            Area = data["Area"].ToString();
            Controller = data["Controller"].ToString();
            Action = data["Action"].ToString();
            Parameters = data["Parameters"].ToString();
            Parent = data["Parent"].ToString();
            Code = data["Code"].ToString();
            Name = data["Name"].ToString();
            Icon = data["Icon"].ToString();
            if (data.Table.Columns.Contains("Owner"))
            {
                Owner = Convert.ToInt32(data["Owner"]);
            }
            if (data.Table.Columns.Contains("Sort"))
            {
                Sort = data["Sort"].ToString();
            }
            if (data.Table.Columns.Contains("ParentFunctionName"))
            {
                ParentFunctionName = data["ParentFunctionName"].ToString();
            }
        }
    }
}