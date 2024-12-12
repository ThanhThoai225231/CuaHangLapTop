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


namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class AdminMenuController : Controller
    {
        private readonly QuanLyNhaHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminMenuController(QuanLyNhaHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

       
        public IActionResult Index(int? page, string search, string foodGroup, int pageSize = 3)
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin"))
            {


                // Lấy dữ liệu từ cơ sở dữ liệu
                var foodItems = _context.MonAns
                    .OrderBy(m => m.SoLuong)
                    .Include(m => m.OCung)
                    .Include(m => m.RAM)
                    .Include(m => m.CPU)
                    .Include(m => m.ManHinh)
                    .Where(item => item.TrangThaiMA == 1);

                if (!string.IsNullOrEmpty(search))
                {
                    foodItems = foodItems.Where(item =>
                    item.TenMon.Contains(search) ||    
                    item.GiaBan.ToString().Contains(search) ||
                    item.SoLuong.ToString().Contains(search) ||
                    item.MaMon.ToString().Contains(search) ||
                    
                   
                    item.MaNhom.ToString().Contains(search) ||
                    item.TenNhom.Contains(search));               
                    
                }

                if (!string.IsNullOrEmpty(foodGroup))
                {
                    foodItems = foodItems.Where(item => foodGroup.Contains(item.TenNhom));
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
                ViewBag.FoodGroup = foodGroup;

                // Các dòng khác để đặt ViewBag cho thông tin phân trang và dữ liệu
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
            ViewBag.NhomMonAnList = _context.NhomMonAns.ToList();
            ViewBag.OCungList = _context.OCungs.ToList();
            ViewBag.ManHinhList = _context.ManHinhs.ToList();
            ViewBag.CPUList = _context.CPUs.ToList();
            ViewBag.RamList = _context.RAMs.ToList();
            return View();
        }

        // Action để xử lý việc thêm món ăn (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MonAn menuItem, IFormFile HinhAnhFile, IFormFile HinhAnhFile1, IFormFile HinhAnhFile2, IFormFile HinhAnhFile3)
        {
            if (_context.MonAns.Any(m => m.MaMon == menuItem.MaMon))
            {
                ModelState.AddModelError("MaMon", "Mã món đã tồn tại trong CSDL.");
                ViewBag.NhomMonAnList = _context.NhomMonAns.ToList();
                ViewBag.OCungList = _context.OCungs.ToList();
                ViewBag.ManHinhList = _context.ManHinhs.ToList();
                ViewBag.CPUList = _context.CPUs.ToList();
                ViewBag.RamList = _context.RAMs.ToList();
                return View(menuItem); // Trả về view với thông báo lỗi
            }

            menuItem.TrangThaiMA = 1; // Đặt trạng thái món ăn
            
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

            if (HinhAnhFile1 != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile1.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile1.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                menuItem.HinhAnh1 = fileName;
            }

            if (HinhAnhFile2 != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile2.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile2.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                menuItem.HinhAnh2 = fileName;
            }
            if (HinhAnhFile3 != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile3.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile3.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                menuItem.HinhAnh3 = fileName;
            }
            _context.MonAns.Add(menuItem);
                _context.SaveChanges();                     
            ViewBag.NhomMonAnList = _context.NhomMonAns.ToList();
            ViewBag.OCungList = _context.OCungs.ToList();
            ViewBag.ManHinhList = _context.ManHinhs.ToList();
            ViewBag.CPUList = _context.CPUs.ToList();
            ViewBag.RamList = _context.RAMs.ToList();
            return RedirectToAction("Index", "AdminMenu");
        }


        public IActionResult Details(int id)
        {
            var menuItem = _context.MonAns
                .Include(m => m.OCung)
                .Include(m => m.RAM)
                .Include(m => m.CPU)
                .Include(m => m.ManHinh)
                .FirstOrDefault(m => m.MaMon == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
            return View(menuItem);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var menuItem = _context.MonAns.FirstOrDefault(m => m.MaMon == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            ViewBag.NhomMonAnList = _context.NhomMonAns.ToList();

            // Lấy danh sách nhóm món ăn

            ViewBag.OCungList = _context.OCungs.ToList();
            ViewBag.ManHinhList = _context.ManHinhs.ToList();
            ViewBag.CPUList = _context.CPUs.ToList();
            ViewBag.RamList = _context.RAMs.ToList();
            ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
            return View(menuItem);
        }


        // Action để xử lý việc cập nhật món ăn (POST)
        [HttpPost]


      
        public IActionResult Edit(int id, MonAn menuItem, IFormFile HinhAnhFile, IFormFile HinhAnhFile1, IFormFile HinhAnhFile2, IFormFile HinhAnhFile3  )
        {
            // Kiểm tra xem món ăn có tồn tại trong cơ sở dữ liệu hay không
            var existingMenuItem = _context.MonAns.Find(id);
            if (existingMenuItem == null)
            {
                return NotFound(); // Trả về trang lỗi hoặc thông báo lỗi nếu món ăn không tồn tại
            }


           




            // Cập nhật thuộc tính của existingMenuItem từ menuItem
                existingMenuItem.TenMon = menuItem.TenMon;
                existingMenuItem.MaMon = menuItem.MaMon;
                existingMenuItem.SoLuong = menuItem.SoLuong;
                
                existingMenuItem.GiaBan = menuItem.GiaBan;
                existingMenuItem.MoTaNgan = menuItem.MoTaNgan;
                existingMenuItem.MoTaDai = menuItem.MoTaDai;
                

                existingMenuItem.TenNhom = Request.Form["TenNhom"];

                existingMenuItem.MaNhom = menuItem.MaNhom;
                existingMenuItem.MaOC = menuItem.MaOC;
                existingMenuItem.MaMH = menuItem.MaMH;
                existingMenuItem.MaCPU = menuItem.MaCPU;
                existingMenuItem.MaRam = menuItem.MaRam;

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

            if (HinhAnhFile1 != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile1.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile1.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                existingMenuItem.HinhAnh1 = fileName;
            }

            if (HinhAnhFile2 != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile2.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile2.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                existingMenuItem.HinhAnh2 = fileName;
            }
            if (HinhAnhFile3 != null)
            {
                // Lưu tệp ảnh vào thư mục trên máy chủ
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = HinhAnhFile3.FileName; // Sử dụng giá trị nguyên gốc của HinhAnhFile.FileName

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnhFile3.CopyTo(stream);
                }

                // Cập nhật tên tệp ảnh trong chuỗi trong cơ sở dữ liệu
                existingMenuItem.HinhAnh3 = fileName;
            }

            _context.SaveChanges(); 
                // Lưu thay đổi
                return RedirectToAction("Edit"); // Chuyển hướng về trang danh sách sau khi cập nhật thành công.
           

            // Trả lại trang chỉnh sửa với dữ liệu hiện tại nếu có lỗi hợp lệ.
        }




        // Hàm kiểm tra món ăn tồn tại
        private bool MenuItemExists(int id)
        {
            return _context.MonAns.Any(e => e.MaMon == id);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var menuItem = _context.MonAns.FirstOrDefault(m => m.MaMon == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            // Cập nhật TrangThaiMA thành 0
            menuItem.TrangThaiMA = 0;

            _context.SaveChanges(); // Lưu thay đổi

            // Chuyển hướng về trang danh sách sau khi cập nhật thành công
            return RedirectToAction("Index");
        }

        public IActionResult TopSellingProducts()
        {
            DateTime currentDate = DateTime.Now;
            DateTime startDate = currentDate.AddDays(-5);

            var topSellingProducts = _context.MonAns
                .Where(food => food.ChiTietHds
                    .Any(orderDetail => orderDetail.MaHdNavigation.NgayHd >= startDate && orderDetail.MaHdNavigation.NgayHd <= currentDate))
                .OrderByDescending(food => food.ChiTietHds
                    .Where(orderDetail => orderDetail.MaHdNavigation.NgayHd >= startDate && orderDetail.MaHdNavigation.NgayHd <= currentDate)
                    .Sum(orderDetail => orderDetail.SoLuongCt))
                .Take(5)
                .ToList();

            var viewModel = new TopSellingProductsViewModel
            {
                TopSellingProducts = topSellingProducts
            };

            return PartialView("_TopSellingProductsPartial", viewModel);
        }

    }
}
