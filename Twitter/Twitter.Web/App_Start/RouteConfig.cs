using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Twitter.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CategoryTweets",
                url: "Categories/{name}",
                defaults: new { controller = "Categories", action = "Index" }
            );

            routes.MapRoute(
                name: "All Users",
                url: "Users",
                defaults: new { controller = "Users", action = "AllUsers" }
            );

            routes.MapRoute(
                name: "UserTweets",
                url: "{username}/UserTweets",
                defaults: new { controller = "Users", action = "UserTweets" }
            );

            routes.MapRoute(
                name: "UserFavoriteTweets",
                url: "{username}/UserFavoriteTweets",
                defaults: new { controller = "Users", action = "UserFavoriteTweets" }
            );

            routes.MapRoute(
                name: "User",
                url: "User/{username}",
                defaults: new { controller = "Users", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
