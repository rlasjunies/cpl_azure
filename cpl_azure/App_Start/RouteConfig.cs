using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cpl_azure
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "AboutMe", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "StaticFiles",
                url: "Content/uploads/{id}",
                defaults: new { controller = "StaticFiles", action = "StaticFileDownload", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Default",
               url: "*",
               defaults: new { controller = "SPA", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}