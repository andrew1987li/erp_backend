using System;

namespace Tw.Com.Kooco.Admin.Misc {
    public enum ResponseType {
        HTML = 0,
        JSON,
        JSON_TEXT
    }

    public class AuthAttribute : Attribute {
        private string _Name;
        private string _Description;

        public string Name {
            get { return _Name ?? (_Name = string.Empty); }
            set { _Name = value; }
        }

        public string Description {
            get { return _Description ?? (_Description = string.Empty); }
            set { _Description = value; }
        }

        /// <summary>
        /// 預設權限
        /// </summary>
        /// <value>
        ///   <c>true</c> 登入後就能擁有的權限，不需透過後台設定，例如修改密碼與登出等等
        ///   <c>false</c> 需由後台設定才能擁有的權限
        /// </value>
        public bool IsDefault { get; set; }

        public ResponseType Type { get; set; }

        public string AllowIP {
            get { return null; }
            set {
                if (value != null) {
                    AllowIpList = value.Split(',', ';');
                }
            }
        }

        public string[] AllowIpList { get; private set; } = { };
    }
}