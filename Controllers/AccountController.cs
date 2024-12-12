using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using OpenCvSharp;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using ThanhThoaiRestaurant.Services;

namespace ThanhThoaiRestaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyNhaHangContext _context;
        private readonly IMailService _mailService;

        public AccountController(QuanLyNhaHangContext context, IMailService _MailService)
        {
            _context = context;
            
            _mailService = _MailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string tenDangNhap, string matKhau)
        {
            var user = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);

            if (user != null && user.MatKhau == matKhau)
            {
                HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
                HttpContext.Session.SetString("VaiTro", user.VaiTro);

                HttpContext.Session.SetString("IsLoggedIn", "true");

                if (user.VaiTro == "KhachHang")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (user.VaiTro == "Admin" || user.VaiTro == "LeTan" || user.VaiTro == "ThuNgan")
                {
                    // Nếu vai trò là "Admin," chuyển hướng đến trang chủ của khu vực "Admin"
                    return RedirectToAction("Index", "Home");
                }
            }

            // Đăng nhập thất bại, hiển thị thông báo lỗi bằng SweetAlert
            TempData["Error"] = "Thông tin đăng nhập không hợp lệ.";
            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Information()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Xóa tất cả các session liên quan đến đăng nhập
            HttpContext.Session.Clear();

            // Sau đó, chuyển hướng đến trang đăng ký hoặc bất kỳ trang nào bạn muốn
            return RedirectToAction("Login");
        }

        public async Task Login1()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                var email = result.Principal.FindFirstValue(ClaimTypes.Email);
                var name = result.Principal.FindFirstValue(ClaimTypes.Name);
              


                // Kiểm tra xem EmailKh đã tồn tại trong bảng NguoiDung chưa
                var existingUser = _context.NguoiDungs.FirstOrDefault(u => u.EmailNd == email);
                if (existingUser == null)
                {
                    // Nếu người dùng không tồn tại, tạo mới người dùng và đăng nhập
                    var newUser = new NguoiDung
                    {
                        TenDangNhap = name,
                        MatKhau = "Abc123", // Bạn cần xác định mật khẩu ở đây hoặc sử dụng mật khẩu mặc định
                        EmailNd = email,
                        VaiTro = "KhachHang" // Vai tro mac dinh khi tao moi nguoi dung
                    };

                    _context.NguoiDungs.Add(newUser);
                    _context.SaveChanges();

                    var newCustomer = new KhachHang
                    {
                        MaKH = GenerateRandomCustomerCode(),
                        TenKh = name,
                        NgayTg = DateTime.Now,
                        DoanhSo = 0,
                        NgaySinhKh = DateTime.Now,
                        GioiTinhKh = "Nam",
                        EmailKh = email,
                        Sdtkh = "0111111111",
                        DiaChiKh = "Ha Noi",
                        DiemTichLuy = 0,
                        TenDangNhap = name
                    };

                    _context.KhachHangs.Add(newCustomer);
                    _context.SaveChanges();
                    // Đăng nhập người dùng mới tạo
                    await SignInUser(newUser);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Nếu người dùng đã tồn tại, đăng nhập người dùng
                    await SignInUser(existingUser);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        

        
        public async Task FacebookLogin()
        {
            await HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponse")
            });
        }

        
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Trích xuất email từ các claim
            var emailClaim = result.Principal.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
            {
                // Không tìm thấy claim email, xử lý lỗi tại đây nếu cần
                return BadRequest("Không thể trích xuất email từ tài khoản Facebook.");
            }

            var email = emailClaim.Value;

            // Kiểm tra xem email đã tồn tại trong CSDL hay chưa
            var existingUser = _context.NguoiDungs.FirstOrDefault(u => u.EmailNd == email);

            if (existingUser == null)
            {
                // Tạo mới người dùng nếu email chưa tồn tại
                var nameClaim = result.Principal.FindFirst(ClaimTypes.Name);
                var name = nameClaim != null ? nameClaim.Value : email;

                var newUser = new NguoiDung
                {
                    TenDangNhap = name,
                    MatKhau = "Abc123", // Bạn cần xác định mật khẩu ở đây hoặc sử dụng mật khẩu mặc định
                    EmailNd = email,
                    VaiTro = "KhachHang" // Vai trò mặc định khi tạo mới người dùng
                };

                _context.NguoiDungs.Add(newUser);
                _context.SaveChanges();

                // Tạo mới khách hàng
                var newCustomer = new KhachHang
                {
                    MaKH = GenerateRandomCustomerCode(),
                    TenKh = name,
                    NgayTg = DateTime.Now,
                    DoanhSo = 0,
                    NgaySinhKh = DateTime.Now,
                    GioiTinhKh = "Nam",
                    EmailKh = email,
                    Sdtkh = "0111111111",
                    DiaChiKh = "Ha Noi",
                    DiemTichLuy = 0,
                    TenDangNhap = name
                };

                _context.KhachHangs.Add(newCustomer);
                _context.SaveChanges();

                // Đăng nhập người dùng mới tạo
                await SignInUser(newUser);
            }
            else
            {
                // Đăng nhập người dùng đã tồn tại
                await SignInUser(existingUser);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]


        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                    // Kiểm tra xem tên đăng nhập đã tồn tại hay chưa
                    var existingUser = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap);

                    if (existingUser != null)
                    {
                        ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại");
                        return View(model);
                    }

                    // Kiểm tra xem email đã tồn tại hay chưa
                    var existingEmail = _context.NguoiDungs.FirstOrDefault(u => u.EmailNd == model.EmailNd);

                    if (existingEmail != null)
                    {
                        ModelState.AddModelError("EmailNd", "Email đã tồn tại");
                        return View(model);
                    }

                    var newUser = new NguoiDung
                    {
                        TenDangNhap = model.TenDangNhap,
                        MatKhau = model.MatKhau,
                        EmailNd = model.EmailNd,
                        VaiTro = "KhachHang" // Mặc định là KhachHang
                    };

                    _context.NguoiDungs.Add(newUser);
                    _context.SaveChanges();

                    var newCustomer = new KhachHang
                    {
                        MaKH = GenerateRandomCustomerCode(),
                        TenKh = model.TenKh,
                        NgayTg = DateTime.Now,
                        DoanhSo = 0,
                        NgaySinhKh = model.NgaySinhKh,
                        GioiTinhKh = model.GioiTinhKh,
                        EmailKh = model.EmailNd,
                        Sdtkh = model.Sdtkh,
                        DiaChiKh = model.DiaChiKh,
                        DiemTichLuy = 0,
                        TenDangNhap = model.TenDangNhap
                    };

                    _context.KhachHangs.Add(newCustomer);
                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
               
            

            return View("Register");
        }



        [HttpPost]
        public IActionResult Information(KhachHang model)
        {
            if (ModelState.IsValid)
            {
               
                var khachHang = new KhachHang
                {
                    TenKh = model.TenKh,
                    NgayTg = DateTime.Now, 
                    DoanhSo = 0, 
                    NgaySinhKh = model.NgaySinhKh,
                    GioiTinhKh = model.GioiTinhKh,
                    EmailKh = model.EmailKh,
                    Sdtkh = model.Sdtkh,
                    DiaChiKh = model.DiaChiKh,
                    DiemTichLuy = 0, 
                    TenDangNhap = model.TenDangNhap 
                };

                
               

                
                _context.KhachHangs.Add(khachHang);
                _context.SaveChanges();

                
                return RedirectToAction("Login");
            }

           
            return View("Register");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAndSendEmail(string email)
        {
            var existingUser = _context.NguoiDungs.FirstOrDefault(u => u.EmailNd == email);
            
            if (existingUser == null)
            {
                // Hiển thị thông báo cho người dùng rằng tài khoản chưa được đăng ký
                TempData["Message"] = "Tài khoản chưa được đăng ký. Vui lòng kiểm tra lại địa chỉ email hoặc đăng ký tài khoản mới.";

                return RedirectToAction("ForgotPassword"); // Hoặc chuyển hướng đến trang đăng ký tài khoản
            }

            var newPassword = "@Abc123";

            // Gửi email với thông tin mật khẩu mới
            var mailData = new MailData
            {
                ReceiverName = email, // Sử dụng email làm tên người nhận để tránh lỗi
                ReceiverEmail = email,
                Title = "Xác nhận và Mật khẩu mới",
                Body = "Xin chào,\n\n"
                     + "Chúng tôi đã nhận được yêu cầu xác nhận và cung cấp mật khẩu mới cho tài khoản của bạn.\n\n"
                     + "Tên đăng nhập: " + existingUser.TenDangNhap + "\n"
                     + "Mật khẩu mới: " + newPassword + "\n\n"
                     + "Vui lòng thay đổi mật khẩu sau khi đăng nhập thành công.\n\n"
                     + "Trân trọng,\n"
                     + "Cửa hàng của chúng tôi"
            };

            // Gửi email
            _mailService.SendMail(mailData);

            // Lưu mật khẩu mới vào CSDL
            existingUser.MatKhau = newPassword;
           
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> UpDateProfile()
        {
            // Lấy tên đăng nhập từ session hiện tại
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            // Truy vấn cơ sở dữ liệu để lấy thông tin từ cả hai bảng
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);
            var user1 = await _context.KhachHangs.FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);

            if (user == null)
            {
                // Xử lý khi không tìm thấy thông tin người dùng
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                TenDangNhap = user.TenDangNhap,
                MatKhau = user.MatKhau,
                TenKh = user1.TenKh,
                DiaChiKh = user1.DiaChiKh,
                Sdtkh = user1.Sdtkh,
                EmailNd = user.EmailNd,
                EmailKh = user.EmailNd,
                GioiTinhKh = user1.GioiTinhKh,
                NgaySinhKh = user1.NgaySinhKh

            };

            // Trả về view với thông tin người dùng để hiển thị và chỉnh sửa
            return View(userViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UpDateProfile(UserViewModel model)
        {
            // Tìm người dùng trong cả hai bảng NguoiDung và KhachHangs
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            // Truy vấn cơ sở dữ liệu để lấy thông tin từ cả hai bảng
            var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);
            var user1 = await _context.KhachHangs.FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);

            // Cập nhật thông tin người dùng nếu có sự thay đổi
            if (!string.IsNullOrEmpty(model.TenDN))
            {
                user.TenDangNhap = model.TenDN;
                user1.TenDangNhap = model.TenDN;
            }
            if (!string.IsNullOrEmpty(model.MatKhau))
            {
                user.MatKhau = model.MatKhau;
            }
            // Cập nhật thông tin khách hàng nếu có sự thay đổi
            if (!string.IsNullOrEmpty(model.TenKh))
            {
                user1.TenKh = model.TenKh;
            }

            if (!string.IsNullOrEmpty(model.DiaChiKh))
            {
                user1.DiaChiKh = model.DiaChiKh;
            }

            if (!string.IsNullOrEmpty(model.Sdtkh))
            {
                user1.Sdtkh = model.Sdtkh;
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                user.EmailNd = model.Email;
                user1.EmailKh = model.Email;
            }
            // Lưu các thay đổi vào cơ sở dữ liệu
            _context.Update(user);
            _context.Update(user1);
            await _context.SaveChangesAsync();

            return RedirectToAction("UpDateProfile", "Account"); // Chuyển hướng đến trang profile sau khi cập nhật thành công
        }





        public int GenerateRandomCustomerCode()
        {
            Random random = new Random();
            int customerCode;

            do
            {
                customerCode = random.Next(1000, 9999); // Sinh số ngẫu nhiên có 4 chữ số
            } while (_context.KhachHangs.Any(kh => kh.MaKH == customerCode)); // Kiểm tra xem mã đã tồn tại trong CSDL hay chưa

            return customerCode;
        }

        private async Task SignInUser(NguoiDung user)
        {
            // Lưu thông tin người dùng vào Session
            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", user.VaiTro);
            HttpContext.Session.SetString("IsLoggedIn", "true");

            // Đăng nhập người dùng
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.TenDangNhap),
        new Claim(ClaimTypes.Email, user.EmailNd),
        new Claim(ClaimTypes.Role, user.VaiTro)
    };
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        private string GenerateRandomPassword()
        {
            // Tạo mật khẩu ngẫu nhiên
            // Bạn có thể sử dụng một thuật toán khác nhau để tạo mật khẩu ngẫu nhiên tùy thuộc vào yêu cầu của bạn
            // Ví dụ: tạo một chuỗi ngẫu nhiên với độ dài cố định
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            return newPassword;
        }



    }
}
