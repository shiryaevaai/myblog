namespace EpamTask.MyBlog.WebInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using EpamTask.MyBlog.WebInterface.Models;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            Log4NetManager.InitializeLog4Net();
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
