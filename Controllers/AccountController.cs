using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace ThanhThoaiRestaurant.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyNhaHangContext _context;
        public AccountController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
            return RedirectToAction("Register");
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
                else if (user.VaiTro == "Admin")
                {
                    // Nếu vai trò là "Admin," chuyển hướng đến trang chủ của khu vực "Admin"
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            // Đăng nhập thất bại, hiển thị thông báo lỗi bằng SweetAlert
            TempData["Error"] = "Thông tin đăng nhập không hợp lệ.";
            return RedirectToAction("Login");

        }

        [HttpPost]
        
        
        public IActionResult Register(NguoiDung model)
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

                // Khởi tạo một đối tượng NguoiDung và lưu thông tin từ model
                var newUser = new NguoiDung
                {
                    TenDangNhap = model.TenDangNhap,
                    MatKhau = model.MatKhau,
                    EmailNd = model.EmailNd,
                    VaiTro = "KhachHang" // Mặc định là KhachHang
                    
                };
               
                // Thực hiện lưu dữ liệu vào CSDL sử dụng Entity Framework Core hoặc ORM tương tự
                _context.NguoiDungs.Add(newUser);
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

       
       


    }
}
