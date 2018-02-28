using QuanLyKhachSan.Areas.Admin.Models.AjaxModel;
using QuanLyKhachSan.Areas.Admin.ViewModel;
using QuanLyKhachSan.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        DataEntities db = new DataEntities();
        [Authorize]
        public ActionResult Index()
        {
            Report report = new Report();
            report.Parameter = new Parameter();
            report.Parameter.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            report.Parameter.FromDate = DateTime.Now.Date.AddMonths(-1).ToString("dd/MM/yyyy");
            return View(report);
        }
        [Authorize]
        public ActionResult ViewReport(Parameter parameter)
        {
            Report report = new Report();
            report.Parameter = parameter;
            DateTime fromD,toD;
            if (!DateTime.TryParseExact(parameter.FromDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromD))
                fromD = DateTime.MinValue;
            if (!DateTime.TryParseExact(parameter.ToDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toD))
                toD = DateTime.MaxValue;
            else
                toD = toD.AddDays(1);
            var ios = from io in db.InOuts
                      where io.Status == 1 && fromD <= io.CheckOutTime && io.CheckOutTime < toD
                      group io by io.PayDate into g
                      orderby g.Key ascending
                      select new {
                          Day = g.Key,
                          RoomServiceAmount = g.Sum(o => o.RoomServiceAmount),
                          OtherServiceAmount = g.Sum(o => o.OtherServiceAmount),
                          TotalAmount = g.Sum(o => o.TotalAmount)
                      };
            List<Detail> details = new List<Detail>();
            foreach (var item in ios)
            {
                Detail d = new Detail();
                d.Day = item.Day.ToString("dd/MM/yyyy");
                d.RoomServiceAmount = item.RoomServiceAmount;
                d.OtherServiceAmount = item.OtherServiceAmount;
                d.TotalAmount = item.TotalAmount;
                details.Add(d);
            }
            report.Details = details.ToArray();
            return View("Index",report);
        }
    }
}
