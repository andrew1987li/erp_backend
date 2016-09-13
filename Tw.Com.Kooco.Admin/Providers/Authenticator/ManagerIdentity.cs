using System.Security.Principal;
using Tw.Com.Kooco.Admin.Entitys;

namespace Tw.Com.Kooco.Admin.Providers.Authenticator
{
    public class ManagerIdentity : IIdentity
    {
        public User CurrentUser { get; private set; }

        public ManagerIdentity(long key, string account, string name)
        {
            CurrentUser = new User(DataAccessProvider.User.Detail(account));
        }

        public ManagerIdentity(long key, string account, string name, string nick)
        {
            CurrentUser = new User(DataAccessProvider.User.Detail(account));
        }

        public ManagerIdentity(string account)
        {
            CurrentUser = new User(DataAccessProvider.User.Detail(account));
        }

        public string Name
        {
            get
            {
                return CurrentUser.Account;
            }
        }

        public string AuthenticationType { get; private set; }

        public bool IsAuthenticated
        {
            //帳號不是空字串或是會員流水號不等於0均視為已登入
            get { return (!string.IsNullOrEmpty(CurrentUser.Account) || CurrentUser.IdentityKey > 0); }
        }
    }
}