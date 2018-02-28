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
    public class RoomServicePriceController : BaseController
    {

        DataEntities db = new DataEntities();
        public ActionResult Index()
        {
            var rsp = from p in db.RoomServicePrices
                      orderby new { p.RoomServiceType, p.Minutes }
                      select p;
            return View(rsp);
        }
        #region Edit
        public ActionResult Edit(int id)
        {
            return View(db.RoomServicePrices.SingleOrDefault(p=>p.RoomServicePriceId==id));
        }
        [HttpPost]
        public ActionResult Edit(RoomServicePrice model)
        {
            if (ModelState.IsValid)
            {
                var editPrice = db.RoomServicePrices.SingleOrDefault(p => p.RoomServicePriceId == model.RoomServicePriceId);
                if (editPrice != null)
                {
                    editPrice.RoomServiceType = model.RoomServiceType;
                    editPrice.Minutes = model.Minutes;
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
            var del = db.RoomServicePrices.SingleOrDefault(p => p.RoomServicePriceId == id);
            if (del!=null)
            {
                db.RoomServicePrices.Remove(del);
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
        public ActionResult Create(RoomServicePrice model)
        {
            if (ModelState.IsValid)
            {
                var price = db.RoomServicePrices.SingleOrDefault(p => p.RoomServiceType == model.RoomServiceType && p.Minutes==model.Minutes);
                if (price == null)
                {
                    db.RoomServicePrices.Add(model);
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
