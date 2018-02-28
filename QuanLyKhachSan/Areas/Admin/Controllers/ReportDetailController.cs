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
    public class ReportDetailController : BaseController
    {
        DataEntities db = new DataEntities();
        [Authorize]
        public ActionResult Index()
        {
            ReportDetail report = new ReportDetail();
            report.Parameter = new Parameter();
            report.Parameter.ToDate = DateTime.Now.ToString("dd/MM/yyyy");
            report.Parameter.FromDate = DateTime.Now.Date.AddMonths(-1).ToString("dd/MM/yyyy");
            return View(report);
        }
        [Authorize]
        public ActionResult ViewReport(Parameter parameter)
        {
            ReportDetail report = new ReportDetail();
            report.Parameter = parameter;
            DateTime fromD,toD;
            if (!DateTime.TryParseExact(parameter.FromDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromD))
                fromD = DateTime.MinValue;
            if (!DateTime.TryParseExact(parameter.ToDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toD))
                toD = DateTime.MaxValue;
            else
                toD = toD.AddDays(1);
            var ios = from io in db.InOuts
                      where io.Status == 1 && fromD <= io.PayDate && io.PayDate < toD
                      group io by new {io.PayDate,io.Room.RoomName,io.RoomId} into g
                      orderby g.Key ascending
                      select new {
                          Day = g.Key.PayDate,
                          RoomId = g.Key.RoomId,
                          RoomName = g.Key.RoomName,
                          Count = g.Count(),
                          RoomServiceAmount = g.Sum(o => o.RoomServiceAmount),
                          TotalAmount = g.Sum(o => o.TotalAmount)
                      };
            List<Detail2> details = new List<Detail2>();
            foreach (var inout in ios)
            {
                Detail2 d = new Detail2();
                d.Day = inout.Day.ToString("dd/MM/yyyy");
                d.RoomName = inout.RoomName;
                d.Count = inout.Count;
                d.RoomServiceAmount = inout.RoomServiceAmount;
                d.TotalAmount = inout.TotalAmount;
                d.OtherServiceDetails = new List<OtherServiceDetail>();
                var otherServices = from ioo in db.InOut_OtherServices
                                    where ioo.InOut.Status == 1 && ioo.InOut.PayDate == inout.Day && ioo.InOut.RoomId == inout.RoomId
                                    group ioo by new { ioo.OtherService.ServiceName } into g
                                    orderby g.Key ascending
                                    select new
                                    {
                                        OtherServiceName = g.Key.ServiceName,
                                        Quantity = g.Sum(o=>o.Quantity),
                                        OtherServiceAmount = g.Sum(o=>o.Quantity*o.OtherService.Price),
                                    };
                foreach (var os in otherServices)
                {
                    d.OtherServiceDetails.Add(new OtherServiceDetail() 
                                                { 
                                                    OtherServiceName = os.OtherServiceName,
                                                    Quantity = os.Quantity,
                                                    OtherServiceAmount = os.OtherServiceAmount
                                                });
                }
                details.Add(d);
            }
            report.Details = details.ToArray();
            return View("Index",report);
        }
    }
}
