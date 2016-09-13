using System.Collections.Generic;
using Tw.Com.Kooco.Admin.Entitys;

namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class FunctionParameter : ListParameter
    {
        private Function _function;

        public Function Function
        {
            get { return _function ?? (_function = new Function()); }
            set { _function = value; }
        }

        public List<object> Controllers { get; set; }
    }
}