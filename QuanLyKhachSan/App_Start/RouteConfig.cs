using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyKhachSan
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Cancel",
                "{controller}/{action}/{id}",
                new { controller = "InOut", action = "CancelCheckIn", id = UrlParameter.Optional },
                new[] { "QuanLyKhachSan.Controllers" }
            );

            routes.MapRoute(
               "PostOrther",
                "{controller}/{action}/{id}",
                new { controller = "InOut", action = "PostOtherServices", id = UrlParameter.Optional },
                namespaces: new[] { "QuanLyKhachSan.Controllers" }
            );

            routes.MapRoute(
                "Check",
                "{controller}/{action}/{id}",
                new { controller = "InOut", action = "CheckIn", id = UrlParameter.Optional},
                namespaces: new[] { "QuanLyKhachSan.Controllers" }
            );

            routes.MapRoute(
                "Home",
                "trang-chu/trang-chu/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "VIP",
                "phong/vip/{id}",
                new { controller = "Room", action = "Room", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
