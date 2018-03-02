using System.ComponentModel.DataAnnotations;
namespace QuanLyKhachSan.ViewModel
{
    #region LogOnModel
    public class LogOnModel
    {
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập"), Display(Name = "Tên Đăng Nhập")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu"), DataType(DataType.Password), Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
    }
    #endregion
    #region Register UserModel
    public class UserVM
    {

        [Required(ErrorMessage = "Chưa nhập tên đăng nhập"), Display(Name = "Tên Đăng Nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu"), Display(Name = "Mật Khẩu"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập Lại Mật Khẩu"), DataType(DataType.Password), System.Web.Mvc.Compare("Password", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }       

    }
    #endregion
    #region ChangePassModel
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Chưa nhập mật khẩu cũ"), DataType(DataType.Password), Display(Name = "Mật Khẩu Hiện Tại")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Chưa nhập mật khẩu mới"), DataType(DataType.Password), Display(Name = "Mật Khẩu Mới")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password), Display(Name = "Nhập Lại Mật Khẩu Mới"), Compare("NewPassword", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region UserUpdate
    public class UserUpdate
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Giới tính")]
        public bool? Sex { get; set; }

        [StringLength(20)]
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }


    }
    #endregion
}