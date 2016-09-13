using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Tw.Com.Kooco.Admin.Startup))]

namespace Tw.Com.Kooco.Admin {
    public class Startup {
        public void Configuration(IAppBuilder app) {
            //// 如需如何設定應用程式的詳細資訊，請參閱  http://go.microsoft.com/fwlink/?LinkID=316888
           
        }
    }
}