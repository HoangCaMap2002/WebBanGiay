using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
        public string Email { get; set; }
    }
}
