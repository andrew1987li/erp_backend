using System.Collections.Generic;
using Tw.Com.Kooco.Admin.Entitys;

namespace Tw.Com.Kooco.Admin.Models.Parameters
{
    public class UserParameter : ListParameter
    {
        private User _user;

        private string _returnUrl;

        private string _ip;

        private string _loginServer;

        public User User
        {
            get { return _user ?? (_user = new User()); }
            set { _user = value; }
        }

        public string ReturnUrl
        {
            get { return _returnUrl ?? (_returnUrl = string.Empty); }
            set { _returnUrl = value; }
        }

        public string RemoteIp
        {
            get { return _ip ?? (_ip = string.Empty); }
            set { _ip = value; }
        }

        public string LoginServer
        {
            get { return _loginServer ?? (_loginServer = string.Empty); }
            set { _loginServer = value; }
        }

        public long UserID { get; set; }

        public List<string> RoleIDs { get; set; }

        public List<string> ActionIDs { get; set; }
    }
}