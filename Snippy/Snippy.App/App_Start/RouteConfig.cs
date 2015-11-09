using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Snippy.App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SnippetDetails",
                url: "Snippets/SnippetDetails/{id}",
                defaults: new { controller = "Snippets", action = "SnippetDetails" }
            );

            routes.MapRoute(
                name: "UserProfile",
                url: "Users/UserDetails/{username}",
                defaults: new { controller = "Users", action = "UserDetails" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
