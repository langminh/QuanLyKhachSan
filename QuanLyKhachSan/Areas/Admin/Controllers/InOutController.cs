using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.AjaxModel;
using QuanLyKhachSan.Areas.Admin.ViewModel;
using QuanLyKhachSan.Entity;
using QuanLyKhachSan.Entity.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static QuanLyKhachSan.MvcApplication;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    public class InOutController : BaseController
    {
        DataEntities db = new DataEntities();
        
        public ActionResult Index()
        {
            return View(db.Floors.ToList<Floor>());
        }
        [HttpPost]
        [AjaxAuthorize]
        public JsonResult CheckIn(CheckIn obj)
        {
            try
            {
                InOut newCheckIn = new InOut();
                newCheckIn.RoomId = obj.RoomId;
                newCheckIn.CheckinTime = obj.CheckinTime;
                newCheckIn.CheckOutTime = obj.CheckinTime;
                newCheckIn.PayDate = obj.CheckinTime.Date;
                newCheckIn.Note = obj.Note;
                newCheckIn.OtherServiceAmount = obj.OtherServiceAmount;
                newCheckIn.PersonNumber = obj.PersonNumber;
                newCheckIn.RoomServiceType = obj.RoomServiceType;
                newCheckIn.Status = 0;//checkin
                db.InOuts.Add(newCheckIn);
                db.SaveChanges();
                if (obj.OtherServices!=null)
                    foreach (var item in obj.OtherServices)
                    {
                        if (item.IsDel != true)
                        {
                            InOut_OtherServices newService = new InOut_OtherServices();
                            newService.InOutId = newCheckIn.InOutId;
                            newService.OtherServiceId = item.OtherServiceId;
                            newService.Quantity = item.Quantity;
                            db.InOut_OtherServices.Add(newService);
                        }
                    }
                db.SaveChanges();
                Room room = db.Rooms.FirstOrDefault(r => r.RoomId == newCheckIn.RoomId);
                if (room != null)
                {
                    room.Status = true;// busy
                    db.SaveChanges();
                }
                var result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            }

        }
        [HttpPost]
        [AjaxAuthorize]
        public JsonResult PostOtherServices(CheckIn obj)
        {
            try
            {
                var ios = from io in db.InOuts
                          where io.RoomId == obj.RoomId && io.Status == 0
                          select io;
                if (ios != null && ios.Count() > 0)
                {
                    foreach (var checkout in ios)
                    {

                        if (obj.OtherServices != null)
                            foreach (var item in obj.OtherServices)
                            {
                                checkout.OtherServiceAmount = obj.OtherServiceAmount;

                                var oldService = checkout.InOut_OtherServices.SingleOrDefault(s => s.OtherServiceId == item.OtherServiceId);
                                if (oldService != null)
                                {
                                    if (item.IsDel == true)
                                    {
                                        db.InOut_OtherServices.Remove(oldService);
                                    }
                                    else
                                    {
                                        oldService.Quantity = item.Quantity;
                                    }
                                }
                                else
                                {
                                    if (item.IsDel != true)
                                    {
                                        InOut_OtherServices newService = new InOut_OtherServices();
                                        newService.InOutId = checkout.InOutId;
                                        newService.OtherServiceId = item.OtherServiceId;
                                        newService.Quantity = item.Quantity;
                                        db.InOut_OtherServices.Add(newService);
                                    }
                                }
                            }
                    }
                    db.SaveChanges();
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            }

        }
        [HttpPost]
        [AjaxAuthorize]
        public JsonResult CancelCheckIn(CheckIn obj)
        {
            try
            {
                var ios = from io in db.InOuts
                          where io.RoomId == obj.RoomId && io.Status == 0
                          select io;
                if (ios != null && ios.Count() > 0)
                {
                    foreach (var io in ios)
                    {
                        if (io != null)
                        {
                            io.Status = 2;// cancel
                            Room room = db.Rooms.FirstOrDefault(r => r.RoomId == io.RoomId);
                            if (room != null)
                            {
                                room.Status = false;//available
                                
                            }
                        }  
                    }
                    db.SaveChanges();
                }
                var result = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            }

        }
        [HttpGet]
        [AjaxAuthorize]
        public JsonResult GetCheckOutInfo(int roomId)
        {
            //try
            //{
                var ios = from io in db.InOuts
                          where io.RoomId == roomId && io.Status == 0
                          select io;
                if (ios != null)
                {
                    CheckOut checkout = new CheckOut();
                    foreach (var io in ios)
                    {
                        checkout.CheckinTime = io.CheckinTime;
                        checkout.CheckInTimeStr = io.CheckinTime.ToString("dd/MM/yyyy HH:mm");
                        checkout.Note = io.Note;
                        checkout.OtherServiceAmount = io.OtherServiceAmount;
                        checkout.PersonNumber = io.PersonNumber;
                        checkout.RoomId = io.RoomId.Value;
                        checkout.RoomServiceType = io.RoomServiceType;
                        checkout.TotalAmount = io.TotalAmount;
                        checkout.OtherServices = new List<Models.AjaxModel.OtherService>();
                        try
                        {
                            foreach (InOut_OtherServices item in io.InOut_OtherServices)
                            {
                                Models.AjaxModel.OtherService os = new Models.AjaxModel.OtherService();
                                os.OtherServiceId = item.OtherServiceId;
                                os.Quantity = item.Quantity;
                                os.ServiceName = item.OtherService.ServiceName;
                                os.Amount = item.Quantity * item.OtherService.Price;
                                checkout.OtherServices.Add(os);
                            }
                        }
                        catch { }
                    }
                    var result = Json(new { success = true, data = checkout }, JsonRequestBehavior.AllowGet);
                    return result;
                }
                else
                {
                    return Json(new { success = false, ErrInfo = "Không tìm thấy checkin" }, JsonRequestBehavior.AllowGet);
                }
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            //}

        }
        [HttpGet]
        [AjaxAuthorize]
        public JsonResult GetRoomServiceAmount(int? roomServiceType, decimal? lengthStay)
        {
            try
            {
                decimal total = 0;
                var prices = from price in db.RoomServicePrices
                          where price.RoomServiceType == roomServiceType
                          orderby price.Minutes
                          select price;
                List<RoomServicePrice> pricesList = prices.ToList();
                if (prices != null && prices.Count() > 0)
                {
                    ////for (int i = 0; i < pricesList.Count; i++)
                    ////{
                    ////    while (lengthStay <= pricesList[i].Minutes && lengthStay>0)
                    ////    {
                    ////        //total += ((lengthStay - pricesList[i].Minutes) /
                    ////        //    (pricesList[i].Minutes - (i + 1 >= pricesList.Count ? 0 : pricesList[i+1].Minutes)))
                    ////        //    * pricesList[i].Price;
                    ////        total += pricesList[i].Price;
                    ////        if (i + 1 >= pricesList.Count)
                    ////            lengthStay = (lengthStay - pricesList[i].Minutes);
                    ////        else
                    ////            lengthStay = (lengthStay - (pricesList[i].Minutes - pricesList[i+1].Minutes));
                    ////    }
                    ////    if (i == pricesList.Count - 1 && total==0)
                    ////    {
                    ////        total = pricesList[i].Price;
                    ////    }
                    ////}
                    //////////////
                    if(pricesList.Count == 1)
                        total = lengthStay.Value * pricesList[0].Price;
                    else
                    {
                        int i = 0;
                        while (i < pricesList.Count && lengthStay > pricesList[i].Minutes)
                        {
                            total += pricesList[i].Price;
                            i++;
                        }
                        if (i == pricesList.Count)//chạy hết list
                        {
                            decimal soPhutChuaTinh = lengthStay.Value-pricesList[i-1].Minutes;
                            decimal soNhan = soPhutChuaTinh / (pricesList[i - 1].Minutes - pricesList[i - 2].Minutes);
                            if (soNhan % 1 != 0) soNhan = (int)(soNhan + 1);
                            total += soNhan * pricesList[i - 1].Price;
                        }
                        else //chổ này có nghĩa lengthStay <= pricesList[i].Minutes
                        {
                            total += pricesList[i].Price;
                        }
                    }
                }
                var result = Json(new { success = true, RoomServiceAmount = Math.Round(total) }, JsonRequestBehavior.AllowGet);
                return result;
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            }

        }

        [HttpPost]
        [AjaxAuthorize]
        [Authorize(Users = "admin")]
        public JsonResult AddNewRoom(FloorJS floor)
        {
            //return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            try
            {
                if (floor.floorID == 0)
                {
                    //Add new floor
                    try
                    {
                        Floor fl = new Floor();
                        var temp = (from c in db.Floors select c).OrderByDescending(x => x.FloorId).FirstOrDefault();
                        fl.FloorName = "Tầng " + (temp.FloorId + 1);
                        fl.Rooms.Add(new Room() { RoomName = floor.roomName, Status = floor.status, IsDouble = floor.isDouble });
                        db.Floors.Add(fl);
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
                    }
                }
                else
                {
                    //Insert new room to floor
                    try
                    {
                        Floor fl = (from c in db.Floors where c.FloorId == floor.floorID select c).FirstOrDefault();
                        fl.Rooms.Add(new Room() { RoomName = floor.roomName, Status = floor.status, IsDouble = floor.isDouble });
                        db.Floors.Add(fl);
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        public JsonResult PostCheckOut(CheckOut obj)
        {
            try
            {
                var ios = from io in db.InOuts
                          where io.RoomId == obj.RoomId && io.Status == 0
                          select io;
                if (ios != null)
                {
                    foreach (var checkout in ios)
                    {
                        checkout.CheckinTime = obj.CheckinTime;
                        checkout.CheckOutTime = obj.CheckOutTime;
                        checkout.PayDate = obj.CheckinTime.Date;
                        checkout.LengthStay = obj.LengthStay;
                        checkout.Note = obj.Note;
                        checkout.OtherServiceAmount = obj.OtherServiceAmount;
                        checkout.RoomServiceAmount = obj.TotalAmount - obj.OtherServiceAmount;
                        checkout.TotalAmount = obj.TotalAmount;
                        checkout.PersonNumber = obj.PersonNumber;
                        checkout.RoomServiceType = obj.RoomServiceType;
                        if( !obj.IsNotCheckOut)
                            checkout.Status = 1;//checkout
                        if (obj.OtherServices != null)
                            foreach (var item in obj.OtherServices)
                            {
                                var oldService = checkout.InOut_OtherServices.SingleOrDefault(s => s.OtherServiceId == item.OtherServiceId);
                                if (oldService != null)
                                {
                                    if(item.IsDel==true)
                                    {
                                        db.InOut_OtherServices.Remove(oldService);
                                    }
                                    else
                                    {
                                        oldService.Quantity = item.Quantity;
                                    }
                                }
                                else
                                {
                                    if (item.IsDel != true)
                                    {
                                        InOut_OtherServices newService = new InOut_OtherServices();
                                        newService.InOutId = checkout.InOutId;
                                        newService.OtherServiceId = item.OtherServiceId;
                                        newService.Quantity = item.Quantity;
                                        db.InOut_OtherServices.Add(newService);
                                    }
                                }
                            }
                    }
                    db.SaveChanges();
                    if (!obj.IsNotCheckOut)
                    {
                        Room room = db.Rooms.FirstOrDefault(r => r.RoomId == obj.RoomId);
                        if (room != null)
                        {
                            room.Status = false; //available
                            db.SaveChanges();
                        }
                    }
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrInfo = ex }, JsonRequestBehavior.DenyGet);
            }

        }
        [Authorize]
        public ActionResult Bill(int roomId)
        {
            BillModel billModel = new BillModel();
            var ios = from io in db.InOuts
                        where io.RoomId == roomId && io.Status == 0
                        select io;
            if (ios != null && ios.Count() > 0)
            {
                foreach (var io in ios)
                {
                    billModel.CheckInTimeStr = io.CheckinTime.ToString("dd/MM/yyyy HH:mm");
                    billModel.CheckOutTimeStr = io.CheckOutTime.ToString("dd/MM/yyyy HH:mm");
                    billModel.OtherServiceAmount = io.OtherServiceAmount;
                    billModel.PersonNumber = io.PersonNumber;
                    billModel.RoomServiceType = io.RoomServiceType;
                    billModel.TotalAmount = io.TotalAmount;
                    billModel.RoomServiceAmout = io.TotalAmount-io.OtherServiceAmount;
                    billModel.RoomName = io.Room.RoomName;
                    string invoiceNum = io.InOutId.ToString().PadLeft(5, '0');
                    billModel.InvoiceNumber = invoiceNum.Substring(invoiceNum.Length-5);
                    billModel.LengthStay = io.LengthStay;
                    billModel.OtherServices = new List<Models.AjaxModel.OtherService>();
                    foreach (InOut_OtherServices item in io.InOut_OtherServices)
                    {
                        Models.AjaxModel.OtherService os = new Models.AjaxModel.OtherService();
                        os.OtherServiceId = item.OtherServiceId;
                        os.Quantity = item.Quantity;
                        os.ServiceName = item.OtherService.ServiceName;
                        os.Amount = item.Quantity * item.OtherService.Price;
                        billModel.OtherServices.Add(os);
                    }
                }
            }
            return View("Bill", billModel);
        }
    }
}
