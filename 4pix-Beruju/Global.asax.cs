using _4pix_Beruju.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _4pix_Beruju
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{

        //    CommonService cs = new CommonService();
        //    Exception ex = Server.GetLastError(); //self explanatory gets the most recent error  
        //    cs.InsertErrorDetails(ex.ToString());
        //    Server.ClearError(); //self explanatory clears the error   
        //                         //(Required to clear as otherwise user gets to see the default ASP.NET error handlers)  
        //    Response.Redirect("~/Home/DisplayError"); //default redirect.

        //}
    }
}
