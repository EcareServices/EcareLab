using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FullTextSearch.Db;
using FullTextSearch.Migrations;

namespace FullTextSearch
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //using (var context = new WikiContext())
            //{
            //    var configuration = new Configuration();
            //    configuration.Seed(context);
            //}
        }
    }
}
