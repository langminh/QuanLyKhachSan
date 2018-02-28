using System.Web.Mvc;

namespace QuanLyKhachSan.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            //============================Admin==================================//
            context.MapRoute(
                "Info",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Account", action = "Info", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Create",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Account", action = "Create", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "ChangePass",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Account", action = "ChangePass", id = UrlParameter.Optional }
            );

            

            context.MapRoute(
                "LogOff",
                "Admin/{controller}/{action}/{id}",
                new {controller= "Account", action = "LogOff", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

            //===================================InOut=====================================//

            context.MapRoute(
                "Index",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CheckOutInfo",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "GetCheckOutInfo", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CancelCheckIn",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "CancelCheckIn", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "PostOtherServices",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "PostOtherServices", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "CheckIn",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "CheckIn", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "GetRoomServiceAmount",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "GetRoomServiceAmount", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "AddNewRoom",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "AddNewRoom", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "PostCheckOut",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "PostCheckOut", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Bill",
                "Admin/{controller}/{action}/{id}",
                new { controller = "InOut", action = "Bill", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                defaults: new { controller = "InOut", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}