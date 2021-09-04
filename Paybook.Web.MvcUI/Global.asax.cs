using Paybook.ServiceLayer.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Paybook.Web.MvcUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            //var ex = Server.GetLastError();
            ////log the error!
            //ILogger logger = LoggerFactory.Instance;
            //logger.Error(ex.Source, ex);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 20;
        }
    }
}
