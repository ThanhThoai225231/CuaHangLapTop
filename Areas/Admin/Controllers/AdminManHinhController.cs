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

    public class AdminManHinhController : Controller
    {
        private readonly QuanLyNhaHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminManHinhController(QuanLyNhaHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index(int? page, string search, int pageSize = 5)
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin"))
            {


                // Lấy dữ liệu từ cơ sở dữ liệu
                IQueryable<ThanhThoaiRestaurant.Models.ManHinh> foodItems = _context.ManHinhs;


                if (!string.IsNullOrEmpty(search))
                {
                    foodItems = foodItems.Where(item =>
                    item.KichThuoc.Contains(search) ||
                    item.MaMH.ToString().Contains(search));

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
            return View();
        }

        // Action để xử lý việc thêm món ăn (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ManHinh menuItem)
        {
            // Kiểm tra xem MaMH đã tồn tại trong CSDL hay không
            if (_context.ManHinhs.Any(m => m.MaMH == menuItem.MaMH))
            {
                ModelState.AddModelError("MaMH", "Mã màn hình đã tồn tại trong CSDL.");
                return View(menuItem); // Trả về view với thông báo lỗi
            }

            _context.ManHinhs.Add(menuItem);
            _context.SaveChanges();

            return RedirectToAction("Index", "AdminManHinh");
        }



        public IActionResult Details(int id)
        {
            var menuItem = _context.ManHinhs

                .FirstOrDefault(m => m.MaMH == id);

            return View(menuItem);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var menuItem = _context.ManHinhs.FirstOrDefault(m => m.MaMH == id);

            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }


        // Action để xử lý việc cập nhật món ăn (POST)
        [HttpPost]
        public IActionResult Edit(int id, ManHinh menuItem)
        {
            // Kiểm tra xem món ăn có tồn tại trong cơ sở dữ liệu hay không
            var existingMenuItem = _context.ManHinhs.Find(id);
            if (existingMenuItem == null)
            {
                return NotFound(); // Trả về trang lỗi hoặc thông báo lỗi nếu món ăn không tồn tại
            }
            // Cập nhật thuộc tính của existingMenuItem từ menuItem

            existingMenuItem.KichThuoc = menuItem.KichThuoc;


            _context.SaveChanges();
            // Lưu thay đổi
            return RedirectToAction("Edit"); // Chuyển hướng về trang danh sách sau khi cập nhật thành công.


            // Trả lại trang chỉnh sửa với dữ liệu hiện tại nếu có lỗi hợp lệ.
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Tìm ManHinh dựa trên id
            var menuItem = _context.ManHinhs.FirstOrDefault(m => m.MaMH == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            // Kiểm tra xem có MonAn chứa MaMH này không
            var relatedMonAn = _context.MonAns.Any(ma => ma.MaMH == id);
            if (relatedMonAn)
            {
                // Thêm thông báo lỗi và trả về view với thông báo lỗi
                TempData["ErrorMessage"] = "Không thể xóa vì có sản phẩm thuộc màn hình này.";
                return RedirectToAction("Index");
            }

            // Xóa ManHinh
            _context.ManHinhs.Remove(menuItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult HasProducts(int id)
        {
            var hasProducts = _context.MonAns.Any(p => p.MaMH == id);
            return Json(hasProducts);
        }



    }
}
