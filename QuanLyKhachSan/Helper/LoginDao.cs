using QuanLyKhachSan.Entity;
using QuanLyKhachSan.Entity.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyKhachSan.Helper
{
    public class LoginDao
    {
        #region Lấy về User đăng nhập
        public static User UserLogOn()
        {
            DataEntities db = new DataEntities();
            return db.Users.FirstOrDefault(m => m.UserName == System.Web.HttpContext.Current.User.Identity.Name);
        }

        public static User UserLogOn(string username, string password)
        {
            var passMD5 = password.GetMD5();
            DataEntities db = new DataEntities();
            var UserLogOn = db.Users.SingleOrDefault(m => m.UserName == username && m.Password == passMD5);
            return UserLogOn;
        }
        #endregion


        #region Check UserLogOn
        public static User CheckUser(string UserName, string Password)
        {
            var passMD5 = Password.GetMD5();
            DataEntities db = new DataEntities();
            var UserLogOn = db.Users.SingleOrDefault(m => m.UserName == UserName && m.Password == passMD5);
            return UserLogOn;
        }

        public static int CheckUser_1(string UserName, string Password)
        {
            var passMD5 = Password.GetMD5();
            DataEntities db = new DataEntities();
            var result = db.Users.SingleOrDefault(m => m.UserName == UserName);
            if(result == null)
            {
                //Sai tên tài khoản
                return -2;
            }
            else
            {
                if (result.Lock.HasValue)
                {
                    if (result.Lock.Value)
                    {
                        //Tài khoản bị khóa
                        return -1;
                    }
                    else
                    {
                        if(result.Password == passMD5)
                        {
                            //Tài khoản đúng mật khẩu
                            return 1;
                        }
                        else
                        {
                            //Sai mật khẩu
                            return 0;
                        }
                    }
                }
                else
                {
                    //Lỗi trong csdl
                    return -3;
                }
            }
        }
        #endregion
    }
}