using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QuanLyKhachSan.Areas.Admin.Code;
using QuanLyKhachSan.Helper;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonContanst.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "LogOut", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}