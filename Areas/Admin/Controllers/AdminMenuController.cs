using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models; // Đảm bảo rằng bạn đã sử dụng namespace chứa các mô hình
using X.PagedList;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMenuController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public AdminMenuController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page, string search, string[] categories, string[] status, string[] priceRange, int pageSize = 10)
        {
            ViewBag.Search = search; // Đặt giá trị tìm kiếm vào ViewBag

            // Lấy dữ liệu từ cơ sở dữ liệu
            var foodItems = _context.MonAns.Where(item => item.TrangThaiMA == 1);

            // Kiểm tra xem người dùng đã nhập từ khóa tìm kiếm hay chưa
            if (!string.IsNullOrEmpty(search))
            {
                foodItems = foodItems.Where(item => item.TenMon.Contains(search)); // Thay đổi điều kiện tìm kiếm tùy theo trường bạn muốn tìm
            }

            // Áp dụng bộ lọc cho nhóm món ăn, tình trạng và giá tiền dựa trên giá trị đã chọn
            
            if (status != null && status.Length > 0)
            {
                foodItems = foodItems.Where(item => status.Contains(item.SoLuong > 0 ? "conhang" : "hethang"));
            }
            if (priceRange != null && priceRange.Length > 0)
            {
                foreach (var range in priceRange)
                {
                    var priceBounds = range.Split('-');
                    if (priceBounds.Length == 2)
                    {
                        if (int.TryParse(priceBounds[0], out int minPrice) && int.TryParse(priceBounds[1], out int maxPrice))
                        {
                            foodItems = foodItems.Where(item => item.GiaBan >= minPrice && item.GiaBan <= maxPrice);
                        }
                    }
                }
            }

            // Số trang trên mỗi trang
            int pageNumber = page ?? 1;

            // Sử dụng X.PagedList để phân trang dữ liệu
            var pagedList = foodItems.ToList().ToPagedList(pageNumber, pageSize);

            // Đặt ViewBag để hiển thị thông tin phân trang trên view
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            // Đặt giá trị đã chọn của các bộ lọc vào ViewBag
            ViewBag.SelectedCategories = categories;
            ViewBag.SelectedStatus = status;
            ViewBag.SelectedPriceRange = priceRange;

            return View(pagedList);
        }




        public IActionResult Details(int id)
        {
            var menuItem = _context.MonAns.FirstOrDefault(m => m.MaMon == id);

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

            ViewBag.NhomMonAnList = _context.NhomMonAns.ToList(); // Lấy danh sách nhóm món ăn

            ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
            return View(menuItem);
        }


        // Action để xử lý việc cập nhật món ăn (POST)
        [HttpPost]


      
        public IActionResult Edit(int id, MonAn menuItem)
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
                existingMenuItem.DonViTinh = menuItem.DonViTinh;
                existingMenuItem.GiaBan = menuItem.GiaBan;
                existingMenuItem.MoTaNgan = menuItem.MoTaNgan;
                existingMenuItem.MoTaDai = menuItem.MoTaDai;
                existingMenuItem.HinhAnh = menuItem.HinhAnh;

                existingMenuItem.TenNhom = Request.Form["TenNhom"];

                if (int.TryParse(Request.Form["MaNhom"], out int maNhom))
                {
                    existingMenuItem.MaNhom = maNhom;
                }

                _context.SaveChanges(); 
                // Lưu thay đổi
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách sau khi cập nhật thành công.
           

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

    }
}
