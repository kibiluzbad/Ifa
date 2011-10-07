using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ifa.Configuration.Fluentlty;

namespace Ifa.Sample1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{itemsPerPage}/{currentPage}", // URL with parameters
                new
                {
                    controller = "Products",
                    action = "Index"
                }, // Parameter defaults
                new { itemsPerPage = @"\d+", currentPage = @"\d+" });

            routes.MapRoute(
                "Root", // Route name
                "", // URL with parameters
                new
                    {
                        controller = "Products",
                        action = "Index"
                    });
            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Ifa.Configuration.ConfigurationHelper.Configure(new FluentConfiguration()
                                                                .Setup(new IfaConfigurationSetup()
                                                                           .ItemsPerPage(20)
                                                                           .Left(1)
                                                                           .Right(1)
                                                                           .Window(4)));
        }
    }
}