using System.Data;
using System.Collections.Generic;
using Tw.Com.Kooco.Admin.Models.Response;

namespace Tw.Com.Kooco.Admin.Models
{
    public class InternalDataTransferToView : InternalResponse
    {
        public int Page { get; set; }

        public long PageSize { get; set; }

        public string KeyWord { get; set; }

        public DataSet List { get; set; }

        //新增通用的 DataSet List
        public List<DataSet> DataList { get; set; }

        /* public object Detail { get; set; }*/
    }
}