using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Twitch_core
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "User space refresh",
                url: "users/{username}/RefreshDashboard",
                defaults: new
                {
                    controller = "User",
                    action = "RefreshDashboard",
                    username = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "User info",
                url: "users/{username}/{page}",
                defaults: new
                {
                    controller = "User",
                    action = "Show",
                    username = UrlParameter.Optional,
                    page = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "User info edit",
                url: "users/{username}/bio/{page}",
                defaults: new
                {
                    controller = "User",
                    action = "Show",
                    username = UrlParameter.Optional,
                    page = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "User info edit setting",
                url: "users/{username}/settings/{setting}",
                defaults: new
                {
                    controller = "User",
                    action = "Show",
                    setting = UrlParameter.Optional,
                    page = "settings"
                }
            );

            routes.MapRoute(
                name: "Logout",
                url: "account/logout",
                defaults: new
                {
                    controller = "User",
                    action = "LogOut",
                }
            );

            routes.MapPageRoute("Main default", "", "~/WebForms/main.aspx");
            routes.MapPageRoute("Signup", "signup", "~/WebForms/signup.aspx");
            routes.MapPageRoute("Engine", "engine", "~/WebForms/engine.aspx");
            routes.MapPageRoute("Login", "login", "~/WebForms/login.aspx");
        }
    }
}
