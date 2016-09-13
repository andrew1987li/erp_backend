using jIAnSoft.Framework.Configuration;
using log4net;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tw.Com.Kooco.Admin.App_Start;
//using System.Web;   //for HttpContext(Awesome Module),
using GlobalConfiguration = System.Web.Http.GlobalConfiguration;
using System.Web.Http;

namespace Tw.Com.Kooco.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            //string log4netPath = Server.MapPath(string.Format("~/log4net.{0}.config", WebStatus.Version));
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4netPath));
            RouteTable.Routes.MapHttpRoute(
                name:"DefaultApi",
                routeTemplate:"api/{controller}/{action}/{id}",
                defaults:new {id = System.Web.Http.RouteParameter.Optional}
                );



            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            if (!Section.Get.Common.IsTest)
            {
                //new MiscController().Minifier();
            }
            
            new Install().Start();
        }

        //Awesome 模組用到 Error message
        /*
        protected void Application_Error(object sender, EventArgs e)
        {
            //return;
            var exception = Server.GetLastError();
            // Log the exception.
            Response.Clear();

            HttpContext.Current.Response.TrySkipIisCustomErrors = true;

            var httpException = exception as HttpException;

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 505:
                        // Server error.
                        routeData.Values.Add("action", "HttpError505");
                        break;

                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }
            }

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                 new HttpContextWrapper(Context), routeData));
        }*/

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Server.ClearError();
            Response.Write(ex.Message + "\nstack:"+ex.StackTrace);
        }


    }
}