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
                name: "Categories",
                url: "Categories",
                defaults: new { controller = "Categories", action = "Index" }
            );

            routes.MapRoute(
                name: "AddTweetInCategory",
                url: "Categories/AddTweetInCategory/{id}",
                defaults: new { controller = "Categories", action = "AddTweetInCategory" }
            );

            routes.MapRoute(
                name: "EditNewTweet",
                url: "Categories/EditNewTweet/{id}",
                defaults: new { controller = "Categories", action = "EditNewTweet" }
            );

            routes.MapRoute(
                name: "NewTweet",
                url: "Categories/NewTweet/{id}",
                defaults: new { controller = "Categories", action = "NewTweet" }
            );

            routes.MapRoute(
                name: "CategoryTweets",
                url: "Categories/TweetsByCategory/{name}",
                defaults: new { controller = "Categories", action = "TweetsByCategory" }
            );

            routes.MapRoute(
                name: "UserTweets",
                url: "{username}/UserTweets",
                defaults: new { controller = "Users", action = "UserTweets" }
            );

            routes.MapRoute(
                name: "All Users",
                url: "Users",
                defaults: new { controller = "Users", action = "AllUsers" }
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
                name: "SearchUser",
                url: "Users/SearchUser/{SearchString}",
                defaults: new { controller = "Users", action = "SearchUser", SearchString = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserDetails",
                url: "User/UserDetails/{username}",
                defaults: new { controller = "Users", action = "UserDetails" }
            );

            routes.MapRoute(
                name: "EditDetails",
                url: "User/EditDetails/{username}",
                defaults: new { controller = "Users", action = "EditDetails" }
            );

            routes.MapRoute(
                name: "UserNotifications",
                url: "Users/UserNotifications",
                defaults: new { controller = "Users", action = "UserNotifications" }
            );

            routes.MapRoute(
                name: "DeleteUserNotification",
                url: "Users/DeleteNotificationFromUser/{id}",
                defaults: new { controller = "Users", action = "DeleteNotificationFromUser" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
