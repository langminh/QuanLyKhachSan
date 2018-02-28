using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.ViewModel;
using QuanLyKhachSan.Entity;
using QuanLyKhachSan.Entity.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Users = "admin")]
    public class OtherServicePriceController : BaseController
    {

        DataEntities db = new DataEntities();
        public ActionResult Index()
        {
            var rsp = from p in db.OtherServices
                      orderby new { p.ServiceName}
                      select p;
            return View(rsp);
        }
        #region Edit
        public ActionResult Edit(int id)
        {
            return View(db.OtherServices.SingleOrDefault(p => p.OtherServiceId == id));
        }
        [HttpPost]
        public ActionResult Edit(OtherService model)
        {
            if (ModelState.IsValid)
            {
                var editPrice = db.OtherServices.SingleOrDefault(p => p.OtherServiceId == model.OtherServiceId);
                if (editPrice != null)
                {
                    editPrice.ServiceName = model.ServiceName;
                    editPrice.Price = model.Price;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "Không tồn tại dữ liệu";
            }
            
            return View(model);
        }
        #endregion
        #region Delete
        public ActionResult Delete(int id)
        {
            var del = db.OtherServices.SingleOrDefault(p => p.OtherServiceId == id);
            if (del!=null)
            {
                db.OtherServices.Remove(del);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(OtherService model)
        {
            if (ModelState.IsValid)
            {
                var price = db.OtherServices.SingleOrDefault(p => p.ServiceName.Trim().ToUpper() == model.ServiceName.Trim().ToUpper());
                if (price == null)
                {
                    db.OtherServices.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Dữ liệu đã tồn tại";
            return View(model);
        }
        #endregion


    }
}
