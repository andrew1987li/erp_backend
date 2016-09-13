using System;
using System.Data; // for DataSet,
using System.Collections.Specialized; // for StringDictionary,
using System.Collections.Generic;

namespace Tw.Com.Kooco.Admin.Areas.Ammas.Entitys
{
    //通用型，只有Id欄位
    public class Entity
    {
        public long Id { get; set; }
    }

    //建案主表
    public class BuildingCaseEntity : Entity
    {
        public string ProjectName { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }

        //public DataSet List { get; set; }

        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            ProjectName = data["ProjectName"];
            StartDate = string.IsNullOrEmpty(data["StartDate"]) ? "" : DateTime.Parse(data["StartDate"]).ToString("yyyy-MM-dd");
            FinishDate = string.IsNullOrEmpty(data["FinishDate"]) ? "" : DateTime.Parse(data["FinishDate"]).ToString("yyyy-MM-dd");
            Status = data["Status"];
            Address = data["Address"];
        }
    }
    //建案明細
    public class BuildingCaseDetailEntity : Entity
    {
        public long CaseId { get; set; } //建案編號

        public long ConstructorId { get; set; } //承包商編號

        public long TypeId { get; set; } //工程類別
        
        public long ItemId { get; set; } //項目編號

        public int ItemQty { get; set; } //數量

        public int ItemPrice { get; set; } //單價

        public string DateChange { get; set; }

        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            CaseId = Convert.ToInt64(data["CaseId"]);
            ConstructorId = Convert.ToInt64(data["ConstructorId"]);
            TypeId = Convert.ToInt64(data["TypeId"]);
            ItemId = Convert.ToInt64(data["ItemId"]);
            ItemQty = Convert.ToInt32(data["ItemQty"]);
            ItemPrice = Convert.ToInt32(data["ItemPrice"]);
            DateChange = string.IsNullOrEmpty(data["DateChange"]) ? "" : DateTime.Parse(data["DateChange"]).ToString("yyyy-MM-dd");
        }
    }


    //工程項目
    public class ConstructionItemEntity : Entity
    {
        public string Name { get; set; }    //名稱
        public string Unit { get; set; }    //計算單位
        public string Price { get; set; }   //單價
        public string DateChange { get; set; }  //修改日期

        //public DataSet List { get; set; }

        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            Name = data["Name"];
            Unit = data["Unit"];
            Price = data["Price"];
            DateChange = string.IsNullOrEmpty(data["DateChange"]) ? "" : DateTime.Parse(data["DateChange"]).ToString("yyyy-MM-dd");
        }
    }

    //承包商
    public class ConstructorEntity : Entity
    {
        public string Name { get; set; }    //名稱
        public string Tel { get; set; }     //電話
        public string Fax { get; set; }     //傳真
        public string TaxId { get; set; }   //統編
        public string Address { get; set; } //地址
        public string MEMO { get; set; }    //備註
        public DataSet ConsDetailType { get; set; }
        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            Name = data["Name"];
            Tel = data["Tel"];
            Fax = data["Fax"];
            TaxId = data["TaxId"];
            Address = data["Address"];
            //Duty = data["Duty"];
            MEMO = data["MEMO"];
        }
    }

    //工程項目類別
    public class ConstructionType : Entity
    {
        public string Name { get; set; }
    }

    //合約主表
    public class ContractEntity : Entity
    {

        public long CaseId { get; set; } //建案編號 db.bigint

        public long ConstructorId { get; set; } //承包商編號 db.bigint

        public long ConstructionTypeId { get; set; } //工程類別 db.bigint

        public int YearCount { get; set; } //備註 db.int

        public string ContractName { get; set; } //合約編號

        public string ContractDate { get; set; } //立約日期

        public Int16 RetentionMoney { get; set; } //保留金 百分比(原金額乘上此數 再除以100) db.smallint

        public Int16 SpecialRetention { get; set; } //特別保留金 百分比(原金額乘上此數 再除以100) db.smallint

        public string MEMO { get; set; } //備註

        public string ContactPerson { get; set; } //合約聯絡人

        public string CaseName { get; set; } //建案名稱(join)

        public string ConstructorName { get; set; } //承包商名稱(join)

        //=============================================================================//
        //  編輯/新增 頁面的操作變數
                public long SelectedConstructionType { get; set; } //所選擇的工程項目 db.int
                public long SelectedCaseId { get; set; } //所選擇的建案 db.bigint
                public long SelectedConstructorId { get; set; }//所選擇的承包商 db.bigint
        //=============================================================================//
                public DataSet ContractDetail; //此合約的所有工程項目細項
        //=============================================================================//
        public DataSet ConsDetailType { get; set; } //工程項目總表

        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            CaseId = Convert.ToInt64(data["CaseId"]);
            ConstructorId = Convert.ToInt64(data["ConstructorId"]);
            ConstructionTypeId = Convert.ToInt64(data["ConstructionTypeId"]);
            YearCount = Convert.ToInt32(data["YearCount"]);
            ContractName = data["ContractName"];
            RetentionMoney = Convert.ToInt16 (data["RetentionMoney"]);
            SpecialRetention = Convert.ToInt16(data["SpecialRetention"]);
            MEMO = data["MEMO"];
            ContactPerson = data["ContactPerson"];
            ContractDate = string.IsNullOrEmpty(data["ContractDate"]) ? "" : DateTime.Parse(data["ContractDate"]).ToString("yyyy-MM-dd");

            // join 欄位
            CaseName = data["CaseName"];
            ConstructorName = data["ConstructorName"];
        }
    }

    //請款單主表
    public class InvoiceEntity : Entity
    {
        public long ContractId { get; set; }    //合約Id
        
        public string MEMO { get; set; }            //備註

        //=============================================================================//
        public string ContractName { get; set; }    //合約編號 (join)
        public string CaseName { get; set; }        //建案名稱(join)
        public string ConstructorName { get; set; } //承包商名稱(join)
        public string ConstructionType { get; set; } // 工程項目類別(join)
        public string CheckPaidDate { get; set; }   //請款日期(join)
        //=============================================================================//
        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            ContractId = Convert.ToInt64(data["ContractId"]);
            MEMO = data["MEMO"];
        }
    }

    //請款單明細
    public class InvoiceDetailEntity : Entity
    {
        public bool CheckPaid { get; set; }     //已支付

        public long InvoiceId { get; set; }    //請款單Id

        public int Stage { get; set; }          //估驗期次

        public string CheckNumber { get; set; }     //支票號碼

        public DateTime ExamineDate { get; set; }    //估驗日期

        public string MEMO { get; set; }            //備註

        public void Fill(StringDictionary data)
        {
            Id = Convert.ToInt64(data["Id"]);
            Stage = Convert.ToInt32(data["Stage"]);
            ExamineDate = Convert.ToDateTime(data["ExamineDate"]);
            MEMO = data["MEMO"];
            CheckPaid = Convert.ToBoolean(data["CheckPaid"]);
        }
    }

}
