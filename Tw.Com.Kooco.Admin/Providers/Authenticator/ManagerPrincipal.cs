using jIAnSoft.Framework.Security.Principal;
using System.Security.Principal;

namespace Tw.Com.Kooco.Admin.Providers.Authenticator
{
    public class ManagerPrincipal : IManagerPrincipal
    {
        public bool IsInRole(string role)
        {
            return false;
        }

        public IIdentity Identity { get; set; }

        public ManagerPrincipal(string account)
        {
            Identity = new ManagerIdentity(account);
        }

        public ManagerPrincipal(long key, string account, string name)
        {
            Identity = new ManagerIdentity(key, account, name);
        }

        public ManagerPrincipal(long key, string account, string name, string nick)
        {
            Identity = new jIAnSoft.Framework.Security.Principal.ManagerIdentity(key, account, name, nick);
        }
    }
}