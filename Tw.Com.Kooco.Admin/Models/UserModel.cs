using System;
using System.ComponentModel.DataAnnotations;
using Tw.Com.Kooco.Admin.Models.Parameters;
using Tw.Com.Kooco.Admin.Models.Response;
using Tw.Com.Kooco.Admin.Providers;
using Tw.Com.Kooco.Admin.Providers.Authenticator;

namespace Tw.Com.Kooco.Admin.Models
{
    /*
     優秀的程式碼是它自己最好的文件說明。當考慮要添加一個註解時，問問自己，「如何能改進這段代碼，以讓它不需要註解」 - Steve McConnell
     */

    public class UserModel
    {
        /// <summary>
        /// 新帳號註冊
        /// </summary>
        /// <param name="param"></param>
        /// <returns>
        /// >0: "帳號編號"
        /// -1: "資料庫異常,系統錯誤。"
        /// -2: "指定的帳號已被註冊。"
        /// -11: "帳號欄位必填。"
        /// -12: "密碼欄位必填。"
        /// -13: "使用者名稱欄位必填。"
        /// </returns>
        public static GeneralResponse Register(UserParameter param)
        {
            var r = new GeneralResponse();
            if (0.Equals(param.User.Account.Trim(new[] { ' ' }).Length))
            {
                r.Code = "-11";
                return r;
            }
            if (0.Equals(param.User.Password.Trim(new[] { ' ' }).Length))
            {
                r.Code = "-12";
                return r;
            }
            if (0.Equals(param.User.FirstName.Trim(new[] { ' ' }).Length) ||
                0.Equals(param.User.LastName.Trim(new[] { ' ' }).Length))
            {
                r.Code = "-13";
                return r;
            }
            try
            {
                r.Code = DataAccessProvider.User.Insert(param).ToString();
            }
            catch (Exception ex)
            {
                r.Code = "-1";
            }
            return r;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static GeneralResponse Login(UserParameter param)
        {
            return new GeneralResponse
            {
                Ok = new AuthenticatorProvider().Login(param)
            };
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public static GeneralResponse Logout()
        {
            AuthenticatorProvider.Logout();
            return new GeneralResponse { Ok = true };
        }

        /// <summary>
        /// 取得當前使用者資料
        /// </summary>
        /// <returns></returns>
        public static DetailResponse GetCurrentUser()
        {
            var user = ((ManagerIdentity)AuthenticatorProvider.GetUser().Identity).CurrentUser;

            return new DetailResponse
            {
                Ok = user != null,
                Code = "1",
                Data = user == null
                    ? null
                    : new
                    {
                        UserId = user.IdentityKey,
                        user.LastName,
                        user.FirstName,
                        user.Name,
                        user.ProfessionId,
                        user.DepartmentId,
                        user.HospitalDistrictId,
                        user.ProfessionName,
                        user.DepartmentName,
                        user.HospitalDistrictName
                    }
            };
        }

        public class LoginParameter
        {
            [Required]
            [Display(Name = "登入帳號")]
            public string Account { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "密碼")]
            public string Password { get; set; }

            public bool Error { get; set; }

            public string ReturnUrl { get; set; }
        }

        public class RegisterParameter
        {
            private string _ac;

            private string _pw;

            private string _fistName;

            private string _lastName;

            [Required]
            [Display(Name = "登入帳號")]
            public string Account
            {
                get { return string.IsNullOrEmpty(_ac) ? "" : _ac; }
                set { _ac = string.IsNullOrEmpty(value) ? "" : value; }
            }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "密碼")]
            public string Password
            {
                get { return string.IsNullOrEmpty(_pw) ? "" : _pw; }
                set { _pw = string.IsNullOrEmpty(value) ? "" : value; }
            }

            [Required]
            [Display(Name = "使用者姓氏")]
            public string FirstName
            {
                get { return string.IsNullOrEmpty(_fistName) ? "" : _fistName; }
                set { _fistName = string.IsNullOrEmpty(value) ? "" : value; }
            }

            [Display(Name = "使用者名字")]
            public string LastName
            {
                get { return string.IsNullOrEmpty(_lastName) ? "" : _lastName; }
                set { _lastName = string.IsNullOrEmpty(value) ? "" : value; }
            }

            public bool Error { get; set; }

            public string ErrorMsg { get; set; }
        }

        /*
        public class EmployeeRegisterParameter : RegisterParameter
        {
            private string _eno;

            [Required]
            [Display(Name = "員工編號")]
            public string EmployeeNo {
                get { return string.IsNullOrEmpty(_eno) ? "" : _eno; }
                set { _eno = string.IsNullOrEmpty(value) ? "" : value; }
            }

            private string _proid;

            [Required]
            [Display(Name = "職稱")]
            public string ProfessionId {
                get { return string.IsNullOrEmpty(_proid) ? "" : _proid; }
                set { _proid = string.IsNullOrEmpty(value) ? "" : value; }
            }

            private string _depid;

            [Required]
            [Display(Name = "單位")]
            public string DepartmentId {
                get { return string.IsNullOrEmpty(_depid) ? "1" : _depid; }
                set { _depid = string.IsNullOrEmpty(value) ? "1" : value; }
            }

            private string _hdid;

            [Required]
            [Display(Name = "院區")]
            public string HospitalDistrictId {
                get { return string.IsNullOrEmpty(_hdid) ? "" : _hdid; }
                set { _hdid = string.IsNullOrEmpty(value) ? "" : value; }
            }
        }*/
    }
}