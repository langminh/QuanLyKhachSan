using QuanLyKhachSan.Areas.Admin.Code;
using QuanLyKhachSan.Areas.Admin.ViewModel;
using QuanLyKhachSan.Entity;
using QuanLyKhachSan.Entity.EF;
using QuanLyKhachSan.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        DataEntities db = new DataEntities();
        public ActionResult Index()
        {
            return View(db.Users);
        }
        #region LogOn
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                var result = LoginDao.CheckUser_1(model.UserName, model.Password);

                if(result == -3)
                {
                    ViewBag.Message = "Đã có lỗi xảy ra! Không thể đăng nhập. Hãy thử lại sau";
                    return View("LogOn", model);
                }else if(result == -2)
                {
                    ViewBag.Message = "Sai tên tài khoản.";
                    return View("LogOn", model);
                }else if(result == -1)
                {
                    ViewBag.Message = "Tài khoản đã bị khóa. Vui lòng lên hệ với quản trị viên";
                    return View("LogOn", model);
                }else if(result == 0)
                {
                    ViewBag.Message = "Sai mật khẩu. Hãy kiểm tra lại mật khẩu";
                    return View("LogOn", model);
                }
                else
                {
                    //Đăng nhập thành công, phân quyền người dùng truy cập
                    var userLogon = LoginDao.CheckUser(model.UserName, model.Password);
                    
                    if(userLogon != null)
                    {
                        if(userLogon.RoleID.Value == 1)
                        {
                            Session[CommonContanst.USER_SESSION] = new UserLogin() { UserID = userLogon.UserID, UserName = userLogon.UserName };
                            FormsAuthentication.SetAuthCookie(userLogon.UserName, true);

                            return RedirectToAction("Index", "InOut");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(userLogon.UserName, true);
                            return RedirectToAction("Index", "Home", new { area = "" });
                        }
                    }
                }
                ViewBag.Message = "Vui lòng kiểm tra lại tên đăng nhập và mật khẩu";
                return View("LogOn", model);
            }
            return View("LogOn", model);
        }
        #endregion


        #region LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Account");
        }
        #endregion


        #region Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var _OldUserName = db.Users.FirstOrDefault(m => m.UserName == model.UserName);
                if (_OldUserName == null)
                {
                    var _User = new User { UserName = model.UserName, Password = model.Password.GetMD5(), RoleID = 2, Address = string.Empty, FullName = string.Empty, Phone = string.Empty, Sex = true, Lock = false };
                    db.Users.Add(_User);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Tên đăng nhập đã tồn tại";
            return View(model);
        }
        #endregion

        #region Đổi mật khẩu
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(ChangePasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var UserLogOn = LoginDao.UserLogOn();
                if (UserLogOn.Password == model.OldPassword.GetMD5())
                {
                    UserLogOn.Password = model.NewPassword;
                    var update = db.Users.Single(u => u.UserID == UserLogOn.UserID);
                    update.Password = model.NewPassword.GetMD5();
                    db.SaveChanges();
                    ViewBag.Message = "Đổi mật khẩu thành công";
                }
                else
                {
                    ViewBag.Message = "Mật Khẩu cũ không đúng";
                }
                return View();
            }
            return View();
        }
        #endregion

        #region Lấy thông tin
        [HttpGet]
        public ActionResult Info(string id)
        {

            var user = (from c in db.Users where c.UserName == id select c).FirstOrDefault();
            if (user != null)
            {
                UserUpdate userUpdate = new UserUpdate();
                userUpdate.UserID = user.UserID;
                userUpdate.FullName = user.FullName;
                userUpdate.Sex = user.Sex;
                userUpdate.Address = user.Address;
                userUpdate.Phone = user.Phone;
                userUpdate.UserName = user.UserName;
                return View(userUpdate);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Info(UserUpdate user)
        {
            if (ModelState.IsValid)
            {
                var temp = db.Users.SingleOrDefault(x => x.UserID == user.UserID);
                if (temp != null)
                {
                    try
                    {
                        temp.FullName = user.FullName;
                        temp.Address = user.Address;
                        temp.Phone = user.Phone;
                        temp.UserName = user.UserName;
                        temp.Sex = user.Sex;
                        db.SaveChanges();
                        return View(temp);
                    }
                    catch {
                        ViewBag.Message = "Có lỗi xảy ra! Vui lòng thử lại sau";
                        return View();
                    }
                }
            }
            ViewBag.Message = "Tên đăng nhập đã tồn tại";
            return View();
        }
        #endregion

        #region Khoá tài khoản
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Lock = !user.Lock;
            db.SaveChanges();
            return Json(new {
                data = user.Lock
            });
        }
        #endregion

        #region Xóa tài khoản
        public JsonResult Delete(int id)
        {
            bool check = false;
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                check = true;
                return Json(new { success = false, data= check}, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                check = false;
                return Json(new { success = false, ErrInfo = ex, data = check }, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion

    }
}