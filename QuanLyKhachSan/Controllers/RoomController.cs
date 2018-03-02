using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Entity.EF;
using QuanLyKhachSan.Entity;

namespace QuanLyKhachSan.Controllers
{
    public class RoomController : Controller
    {
        private DataEntities db = new DataEntities();
        // GET: Room
        public ActionResult Room(int? id)
        {
            if (id.HasValue)
            {
                var list = db.Rooms.Where(x => x.TypeID == id.Value).ToList();
                return View(list);
            }
            ViewBag.Error = "Nội dung không có sẵn";
            return View();
        }

        public ActionResult Detail(int? id)
        {
            if (id.HasValue)
            {
                var room = db.Rooms.Find(id.Value);
                return View(room);
            }
            ViewBag.Error = "Nội dung không có sẵn";
            return View();
        }
    }
}