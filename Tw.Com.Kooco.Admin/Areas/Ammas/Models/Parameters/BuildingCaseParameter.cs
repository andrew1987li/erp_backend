using Tw.Com.Kooco.Admin.Areas.Ammas.Entitys; // for BuildingCaseEntity,
using Tw.Com.Kooco.Admin.Models.Parameters; // for ListParameter,


namespace Tw.Com.Kooco.Admin.Areas.Ammas.Models.Parameters
{
    //建案
    public class BuildingCaseParameter : ListParameter
    {
        public BuildingCaseEntity Entity { get; set; }
    }
    
    //工程項目細項
    public class ConstructionItemParameter : ListParameter
    {
        public ConstructionItemEntity Entity { get; set; }
    }

    //承包商
    public class ConstructorParameter : ListParameter
    {
        public ConstructorEntity Entity { get; set; }
    }

    //合約
    public class ContractParameter : ListParameter
    {
        public ContractEntity Entity { get; set; }
    }

    //請款單
    public class InvoiceParameter : ListParameter
    {
        public InvoiceEntity Entity { get; set; }
    }

}
