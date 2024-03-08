using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.WebSockets;
using System.Security.Claims;
using WebBanGiay.Models;
using WebBanGiay.Services;
using WebBanGiay.ViewModels;

namespace WebBanGiay.Controllers
{
    public class AccessController : Controller
    {
        private readonly IUserService _userService;
        public AccessController(IUserService userService)
        {
            _userService= userService;
        }
        public async Task<IActionResult> Login()
        {

			ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            
            if (user.Username == null)
            {
                TempData["TK"] = "Tài khoản không được để trống";
            }
            else if (user.Password == null)
            {
                TempData["MK"] = "Mật khẩu không được để trống";
            }
            else
            {
                var item = await _userService.GetUserByUserNameAsync(user.Username);
                if (item != null)
                {
                    int id_user = item.UserId;
                    var usersWithTypes = await _userService.GetAllNameRoleUserAsync(id_user);
                    var passwordHasher = new PasswordHasher();
                    // Kiểm tra mật khẩu nhập vào có khớp với mật khẩu đã mã hoá hay không
                    var passwordVerification = passwordHasher.VerifyHashedPassword(item.Password, user.Password);
                    // Mật khẩu đúng => chuyển hướng đến trang khác
                    if (passwordVerification == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetInt32("UserId", id_user);
                        if (usersWithTypes.Contains("Admin"))
                        {
                            List<Claim> claims = new List<Claim>()
                              {
                              new Claim(ClaimTypes.NameIdentifier, item.Username),
							  new Claim(ClaimTypes.Name, item.Name)
                              };
                            foreach (var role in usersWithTypes)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role)); // Thêm từng quyền vào danh sách claims
                            }
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                      CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity), properties);
                            return Redirect("/Admin/HomeAdmin/Index");
                        }
                        if (usersWithTypes.Contains("Customer"))
                        {
                            List<Claim> claims = new List<Claim>()
                              {
                              new Claim(ClaimTypes.NameIdentifier, item.Username),
                              new Claim(ClaimTypes.Name, item.Name)
                              };
                            foreach (var role in usersWithTypes)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role)); // Thêm từng quyền vào danh sách claims
                            }
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                      CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity), properties);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                // Mật khẩu không khớp hoặc không tìm thấy người dùng
                TempData["SaiMK"] = "Sai tài khoản hoặc mật khẩu";
            }
            // Trả về view Login để nhập lại thông tin đăng nhập
            return View();
        }
      

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM rvm)
        {
            var result = await _userService.Regiter(rvm);
            if (!result)
            {
                TempData["Error"] = "Đã tồn tại tài khoản này rồi hoặc dữ liệu không hợp lệ.";
                return View();
            }

            return RedirectToAction("Login", "Access");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
