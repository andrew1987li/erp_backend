using jIAnSoft.Framework.Security.Principal;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Tw.Com.Kooco.Admin.Providers;

namespace Tw.Com.Kooco.Admin.Entitys
{
    public class User : ManagerCurrentUser
    {
        private static ILog log = LogManager.GetLogger(typeof(User));

        public User()
        {
            OperatePermission = new Dictionary<long, long>();
            OwnFunction = new List<int>();
            AuthPath = new List<string>();
        }

        public User(DataSet argUserDetail)
            : this()
        {
            Fill(argUserDetail);
        }

        public User(StringDictionary argSdUserDetail)
            : this()
        {
            Fill(argSdUserDetail);
            /* if (argSdUserDetail.Count < 1) {
                 return;
             }

             IdentityKey = Convert.ToInt32(argSdUserDetail["UserId"]);
             Account = argSdUserDetail["Account"];
             IsAdministrator = AuthenticatorProvider.IsAdministrator(Account);
             FirstName = argSdUserDetail["FirstName"];
             LastName = argSdUserDetail["LastName"];
             Name = string.Format("{0}{1}", FirstName, LastName);
             Password = argSdUserDetail["Password"];
             ProfessionId = Convert.ToInt16(argSdUserDetail["ProfessionId"]);
             DepartmentId = Convert.ToInt16(argSdUserDetail["DepartmentId"]);
             HospitalDistrictId = Convert.ToInt16(argSdUserDetail["HospitalDistrictId"]);
             ProfessionName = argSdUserDetail["ProfessionName"];
             DepartmentName = argSdUserDetail["DepartmentName"];
             HospitalDistrictName = argSdUserDetail["HospitalDistrictName"];
             EncryptPassword = argSdUserDetail["EncryptPassword"];
             FunctionPerms = argSdUserDetail["Functions"];*/
            //var service = new Provider.Service();
            //如果是管理者擁有所有的服務
            //if (IsAdministrator)
            //{
            //    //當擁有所有的服務權限時，自動增加全域的權限
            //    var s = service.ServiceList.Select(i => string.Format("[{0}]", i.Value.ServiceId.ToString(Section.Get.Common.Culture))).ToList();
            //    s.Insert(0, "[0]");
            //    ServicePerms = string.Join(",", s);
            //}
            //else
            //{
            //    ServicePerms =
            //        string.Format
            //            ("{1}{0}",
            //             argSdUserDetail["ServicePerms"],
            //             (service.ServiceList.Count.Equals(argSdUserDetail["ServicePerms"].Split(',').Count()))
            //                 ? "[0],"
            //                 : ""); //當擁有所有的服務權限時，自動增加全域的權限
        }

        /// <summary>
        /// 帳號可以使用的功能列表
        /// </summary>
        public string Functions { get; set; }

        //自訂帳號的權限
        public string Operations { get; set; }

        //
        /// <summary>
        /// 帳號是否具有最高管理權限。Web.Config內設定
        /// </summary>
        public bool IsAdministrator { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short ProfessionId { get; set; }

        public short DepartmentId { get; set; }

        public short HospitalDistrictId { get; set; }

        public string ProfessionName { get; set; }

        public string DepartmentName { get; set; }

        public string HospitalDistrictName { get; set; }

        public int Status { get; set; }

        public string EmployeeNo { get; set; }

        public string RuleGroups { get; set; }

        public string RoleIDs { get; set; }

        public long PrimaryRule { get; set; }

        public long UserId { get; set; }

        public Dictionary<long, long> OperatePermission { get; set; }

        public List<string> AuthPath { get; set; }

        /// <summary>
        /// 可以顯示的Function 清單
        /// </summary>
        public List<int> OwnFunction { get; set; }

        public static bool isChinese(string strChinese)
        {
            bool bresult = true;
            int dRange = 0;
            int dstringmax = Convert.ToInt32("9fff", 16);
            int dstringmin = Convert.ToInt32("4e00", 16);
            for (int i = 0; i < strChinese.Length; i++)
            {
                dRange = Convert.ToInt32(Convert.ToChar(strChinese.Substring(i, 1)));
                if (dRange >= dstringmin && dRange < dstringmax)
                {
                    bresult = true;
                    break;
                }
                else
                {
                    bresult = false;
                }
            }

            return bresult;
        }

        //[a].[PrimaryRule],[a].[RuleGroups],
        public static User FetchAdminUserDetail(string account)
        {
            return new User(DataAccessProvider.User.Detail(account));
        }

        public static StringDictionary DataTableToStringDictionary(DataTable argSourceTable)
        {
            var data = new StringDictionary();
            if (argSourceTable.Rows.Count <= 0) return data;
            var index = 0;
            foreach (var column in
                argSourceTable.Columns.Cast<DataColumn>().Where(column => !data.ContainsKey(column.ColumnName)))
            {
                data.Add(column.ColumnName,
                    (!argSourceTable.Rows[0].IsNull(index))
                        ? argSourceTable.Rows[0].ItemArray[index].ToString()
                        : "");
                index++;
            }
            return data;
        }

        public string GetUserName()
        {
            if (isChinese(FirstName + LastName))
            {
                return string.Format("{0}{1}", LastName, FirstName);
            }
            else
            {
                return string.Format("{1} {0}", LastName, FirstName);
            }
        }

        public void Fill()
        {
            Fill(DataAccessProvider.User.Detail(IdentityKey));
        }

        public void Fill(DataSet argUserDetail)
        {
            Fill(DataTableToStringDictionary(argUserDetail.Tables[0]));
            foreach (
                var row in
                    argUserDetail.Tables[1].Rows.Cast<DataRow>()
                        .Where(row => !string.IsNullOrEmpty(row["Operations"].ToString())))
            {
                //群組的權限
                BindOperatePermission(row["Operations"].ToString());
                //群組擁有的功能清單
                BindFunction(row["Functions"].ToString());
            }

            foreach (
                var row in
                    argUserDetail.Tables[2].Rows.Cast<DataRow>()
                        .Where(row => !string.IsNullOrEmpty(row["ID"].ToString())))
            {
                var Area = row["Area"].ToString();
                var Controller = row["Controller"].ToString();
                var Action = row["Action"].ToString();

                string path = (Area.Equals("")) ? string.Format("/{0}/{1}", Controller, Action) : string.Format("/{0}/{1}/{2}", Area, Controller, Action);
                AuthPath.Add(path);
                //log.Info(path);
            }
        }

        public void Fill(StringDictionary argSdUserDetail)
        {
            if (argSdUserDetail.Count < 1)
            {
                return;
            }

            UserId = IdentityKey = Convert.ToInt32(argSdUserDetail["UserId"]);
            Account = argSdUserDetail["Account"];
            IsAdministrator = AuthenticatorProvider.IsAdministrator(Account);
            FirstName = argSdUserDetail["FirstName"];
            LastName = argSdUserDetail["LastName"];
            Name = string.Format("{0}{1}", FirstName, LastName);
            Password = argSdUserDetail["Password"];
            ProfessionId = Convert.ToInt16(argSdUserDetail["ProfessionId"]);
            DepartmentId = Convert.ToInt16(argSdUserDetail["DepartmentId"]);
            HospitalDistrictId = Convert.ToInt16(argSdUserDetail["HospitalDistrictId"]);
            ProfessionName = argSdUserDetail["ProfessionName"];
            DepartmentName = argSdUserDetail["DepartmentName"];
            HospitalDistrictName = argSdUserDetail["HospitalDistrictName"];
            EncryptPassword = argSdUserDetail["EncryptPassword"];
            Functions = argSdUserDetail["Functions"];
            Operations = argSdUserDetail["Operations"];
            EmployeeNo = argSdUserDetail["EmployeeNo"];
            Status = Convert.ToInt32(argSdUserDetail["Status"]);
            PrimaryRule = Convert.ToInt64(argSdUserDetail["PrimaryRule"]);
            RuleGroups = argSdUserDetail["RuleGroups"];

            //個人自定義權限
            BindOperatePermission(Operations);
            //個人自定義擁有的功能清單
            BindFunction(Functions);
        }

        /// <summary>
        /// 綁定可顯示的Function 清單
        /// </summary>
        /// <param name="argStrFunctions"></param>
        private void BindFunction(string argStrFunctions)
        {
            if (string.IsNullOrEmpty(argStrFunctions)) return;

            foreach (
                var f in
                    argStrFunctions.Split(new[] { ',' })
                        .Select(function => Convert.ToInt32(function))
                        .Where(f => !OwnFunction.Contains(f)))
            {
                OwnFunction.Add(f);
            }
        }

        //private List<OperationItem> _operationItems;
        /// <summary>
        /// 綁定操作權限
        /// </summary>
        /// <param name="argStrOperations"></param>
        private void BindOperatePermission(string argStrOperations)
        {
            if (string.IsNullOrEmpty(argStrOperations)) return;

            var operationItems = JsonConvert.DeserializeObject<OperationItem[]>(argStrOperations);
            foreach (var item in operationItems)
            {
                if (OperatePermission.ContainsKey(item.Function))
                {
                    OperatePermission[item.Function] = item.Perms | OperatePermission[item.Function];
                }
                else
                {
                    OperatePermission.Add(item.Function, item.Perms);
                }
            }
            // operationItems = null;
        }

        private class OperationItem
        {
            public long Function { get; set; }

            public long Perms { get; set; }
        }

        //ServicePermsToSqlString = ServicePerms.Replace("[", "").Replace("]", "");
        //LastLoginIp = argSdUserDetail["LastLoginIp"];
        //LastLoginTime = argSdUserDetail["LastLoginTime"];
        //OperatePermsValue = Convert.ToInt32(argSdUserDetail["OperatePerms"]);

        //var operatePerms = Convert.ToString(OperatePermsValue, 2).PadLeft(7, '0').ToCharArray();
        //Array.Reverse(operatePerms);
        //OperatePerms.Select = operatePerms[0].Equals('1') | IsAdministrator;
        //OperatePerms.Insert = operatePerms[1].Equals('1') | IsAdministrator;
        //OperatePerms.Update = operatePerms[2].Equals('1') | IsAdministrator;
        //OperatePerms.Delete = operatePerms[3].Equals('1') | IsAdministrator;
        //OperatePerms.Export = operatePerms[4].Equals('1') | IsAdministrator;
        //OperatePerms.View = operatePerms[5].Equals('1') | IsAdministrator;
        //OperatePerms.Execute = operatePerms[6].Equals('1') | IsAdministrator;
    }
}