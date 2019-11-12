using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Student
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["Logged"] = false;
       
            Session["Name"] = "";

            Session["ID"] = 0;

            Session["Role"] = null;

        }

    }
}
