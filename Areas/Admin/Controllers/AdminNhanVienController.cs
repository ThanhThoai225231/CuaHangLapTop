using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models; // Đảm bảo rằng bạn đã sử dụng namespace chứa các mô hình
using X.PagedList;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using NuGet.Configuration;
using System.Data;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNhanVienController : Controller


    {


        private readonly QuanLyNhaHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminNhanVienController(QuanLyNhaHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int? page, string search, string gender, string position, string startDate, string endDate, int pageSize = 3)
        {


            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin"))
            {
                // Lấy dữ liệu từ cơ sở dữ liệu
                var foodItems= _context.NhanViens.AsQueryable();


                // Xử lý tìm kiếm theo từ khóa
                if (!string.IsNullOrEmpty(search))
                {
                    foodItems = foodItems.Where(item =>
     item.MaNv.Contains(search) ||
     item.TenNv.Contains(search) ||
    
     item.EmailNv.Contains(search) ||
     item.GioiTinhNv.Contains(search) ||
   
     item.DiaChiNv.Contains(search) ||
     item.ChucVu.Contains(search) ||
    
     item.TenDangNhap.Contains(search));

                }

                // Xử lý tìm kiếm theo giới tính
                if (!string.IsNullOrEmpty(gender))
                {
                    foodItems = foodItems.Where(item => item.GioiTinhNv.Equals(gender));
                }

                // Xử lý tìm kiếm theo chức vụ
                if (!string.IsNullOrEmpty(position))
                {
                    foodItems = foodItems.Where(item => item.ChucVu.Equals(position));
                }

                if (!string.IsNullOrEmpty(position))
                {
                    foodItems = foodItems.Where(item => position.Contains(item.ChucVu));
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    foodItems = foodItems.Where(item => gender.Contains(item.GioiTinhNv));
                }

                int pageNumber = page ?? 1;
                var pagedList = foodItems.ToPagedList(pageNumber, pageSize);

                int startItem = (pageNumber - 1) * pageSize + 1;
                int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

                int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
                int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
                int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);

                // Đặt ViewBag cho thông tin phân trang
                ViewBag.Search = search;
                ViewBag.Gender = gender;
                ViewBag.Position = position;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;

                // Các đoạn mã khác để đặt ViewBag cho thông tin phân trang và dữ liệu
                // Đặt ViewBag cho thông tin phân trang
                ViewBag.TotalItems = pagedList.TotalItemCount;
                ViewBag.TotalPages = pagedList.PageCount;
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.StartItem = startItem;
                ViewBag.EndItem = endItem;
                ViewBag.MaxVisiblePages = maxVisiblePages;
                ViewBag.StartPage = startPage;
                ViewBag.EndPage = endPage;
                ViewBag.StartPage = startPage;
                ViewBag.EndPage = endPage;
                return View(pagedList);
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }


        public IActionResult Create()
        {
           
            return View();
        }

        // Action để xử lý việc thêm món ăn (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NhanVienViewModel menuItem, IFormFile HinhAnhFile)
        {
            if (_context.NguoiDungs.Any(m => m.TenDangNhap == menuItem.MaNv))
            {
                TempData["TenDangNhapError"] = "Mã nhân viên đã tồn tại trong hệ thống.";
                return View(menuItem); // Trả về view với thông báo lỗi
            }



            if (_context.NhanViens.Any(m => m.MaNv == menuItem.MaNv))
            {
                ModelState.AddModelError("MaNv", "Mã nhân viên đã tồn tại trong hệ thống.");

                return View(menuItem); // Trả về view với thông báo lỗi
            }

            if (string.IsNullOrWhiteSpace(menuItem.EmailNv))
            {
                ViewBag.EmailNvError = "Email không được để trống.";
            }

            var emailRegex = new Regex(@"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,4}$");
            if (!emailRegex.IsMatch(menuItem.EmailNv))
            {
                ViewBag.EmailNvError = "Email phải có dạng xxx@gmail.com.";
            }

            // Kiểm tra lỗi độ dài email
            if (menuItem.EmailNv.Length > 50)
            {
                ViewBag.EmailNvError = "Email không được vượt quá 50 kí tự.";
            }


            if (HinhAnhFile != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                menuItem.HinhAnh = fileName;
            }

            var nguoiDung = new NguoiDung
            {
                TenDangNhap = menuItem.MaNv,
                MatKhau = "Abc@123",
                EmailNd = menuItem.EmailNv,
                VaiTro = menuItem.ChucVu
            };

            _context.NguoiDungs.Add(nguoiDung);
            _context.SaveChanges();

            var nhanVien = new NhanVien
            {
                MaNv = menuItem.MaNv,
                TenNv = menuItem.TenNv,
                NgaySinhNv = menuItem.NgaySinhNv,
                GioiTinhNv = menuItem.GioiTinhNv,
                NgayVl = menuItem.NgayVl,
                ChucVu = menuItem.ChucVu,
                Sdtnv = menuItem.Sdtnv,
                Cccdnv = menuItem.Cccdnv,
                DiaChiNv = menuItem.DiaChiNv,
                EmailNv = menuItem.EmailNv,
                HinhAnh = menuItem.HinhAnh,
                TenDangNhap = menuItem.MaNv
        };


            _context.NhanViens.Add(nhanVien);
            _context.SaveChanges();

           

            return RedirectToAction("Index", "AdminNhanVien");
        }

        public IActionResult Details( string id)
        {
            var nhanVien = _context.NhanViens.FirstOrDefault(dh => dh.MaNv == id);
            var nguoiDung = _context.NguoiDungs.FirstOrDefault(nd => nd.TenDangNhap == id);

            var nhanVienViewModel = new NhanVienViewModel
            {
                MaNv = nhanVien.MaNv,
                TenNv = nhanVien.TenNv,
                NgayVl = nhanVien.NgayVl,
                NgaySinhNv = nhanVien.NgaySinhNv,
                Sdtnv = nhanVien.Sdtnv,
                EmailNv = nhanVien.EmailNv,
                GioiTinhNv = nhanVien.GioiTinhNv,
                DiaChiNv = nhanVien.DiaChiNv,
                ChucVu = nhanVien.ChucVu,
                Cccdnv = nhanVien.Cccdnv,
                TenDangNhap = nhanVien.TenDangNhap
                
            };

            return View(nhanVienViewModel);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin"))
            {
                // Kiểm tra xem nhân viên có tồn tại không
                var nhanVien = _context.NhanViens.FirstOrDefault(dh => dh.MaNv == id);

                if (nhanVien == null)
                {
                    // Trả về trang lỗi hoặc thông báo không tìm thấy nhân viên
                    return NotFound();
                }

               
                    // Xóa nhân viên
                    _context.NhanViens.Remove(nhanVien);
                    _context.SaveChanges();

                    // Chuyển hướng về trang danh sách nhân viên sau khi xóa thành công
                    return RedirectToAction("Index");
               
            }
            else
            {
                return Redirect("/Account/Login");
            }
        }


        [HttpGet]
        public IActionResult Edit(string id)
        {
            var menuItem = _context.NhanViens.FirstOrDefault(m => m.MaNv == id);

            if (menuItem == null)
            {
                return NotFound();
            }

           

            ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
            return View(menuItem);
        }


        // Action để xử lý việc cập nhật món ăn (POST)
        [HttpPost]



        public IActionResult Edit(string id, NhanVien menuItem, IFormFile HinhAnhFile)
        {
            // Kiểm tra xem món ăn có tồn tại trong cơ sở dữ liệu hay không
            var existingMenuItem = _context.NhanViens.Find(id);
            if (existingMenuItem == null)
            {
                return NotFound(); // Trả về trang lỗi hoặc thông báo lỗi nếu món ăn không tồn tại
            }


            // Cập nhật thuộc tính của existingMenuItem từ menuItem
            
            existingMenuItem.TenNv = menuItem.TenNv;
            existingMenuItem.GioiTinhNv = menuItem.GioiTinhNv;
            existingMenuItem.NgayVl = menuItem.NgayVl;
            existingMenuItem.NgaySinhNv = menuItem.NgaySinhNv;
            existingMenuItem.ChucVu = menuItem.ChucVu;
            existingMenuItem.DiaChiNv = menuItem.DiaChiNv;
            existingMenuItem.Sdtnv = menuItem.Sdtnv;
            existingMenuItem.Cccdnv = menuItem.Cccdnv;
            
            

            if (HinhAnhFile != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                existingMenuItem.HinhAnh = fileName;


            }

            if (_context.Entry(existingMenuItem).Property("ChucVu").IsModified)
            {
                // Chức vụ đã thay đổi, cập nhật tương ứng trong bảng NguoiDung
                var nguoiDung = _context.NguoiDungs.FirstOrDefault(nd => nd.TenDangNhap == existingMenuItem.TenDangNhap);
                if (nguoiDung != null)
                {
                    // Cập nhật vai trò trong bảng NguoiDung dựa trên chức vụ mới của Nhân Viên
                    nguoiDung.VaiTro = existingMenuItem.ChucVu;
                }
            }


            _context.SaveChanges();
            // Lưu thay đổi
            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách sau khi cập nhật thành công.


            // Trả lại trang chỉnh sửa với dữ liệu hiện tại nếu có lỗi hợp lệ.
        }

        private string GenerateMaNhanVien()
        {
            // Lấy danh sách mã nhân viên hiện có từ database để kiểm tra trùng lặp
            var existingMaNhanViens = _context.NhanViens.Select(nv => nv.MaNv).ToList();

            string newMaNhanVien;
            Random random = new Random();

            do
            {
                // Sinh một số ngẫu nhiên từ 100 đến 999
                int randomNumber = random.Next(100, 1000);

                // Tạo mã nhân viên mới
                newMaNhanVien = "NV" + randomNumber.ToString("000");
            }
            while (existingMaNhanViens.Contains(newMaNhanVien));

            return newMaNhanVien;
        }

    }
}
