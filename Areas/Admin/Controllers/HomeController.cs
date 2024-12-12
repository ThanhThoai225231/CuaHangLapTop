using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public HomeController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin" || HttpContext.Session.GetString("VaiTro") == "ThuNgan" || HttpContext.Session.GetString("VaiTro") == "LeTan"))
            {

            
                int unreadNotificationCount = _context.ThongBaos.Count(tb => tb.TrangThaiTB == 1);
            // Đếm số lượng khách hàng trong bảng KhachHang
            int customerCount = _context.KhachHangs.Count();

            // Tính tổng số hóa đơn có TrangThaiHD = 2
            int totalPaidInvoicesCount = _context.HoaDons
                .Count(hd => hd.TrangThaiHD == 2);

            // Tính tổng TiềnTT từ tất cả các hóa đơn có TrangThaiHD = 2
            double totalAmountPaid = _context.HoaDons
                .Where(hd => hd.TrangThaiHD == 2)
                .Sum(hd => hd.TienTt);

            // Truyền số lượng khách hàng, tổng số hóa đơn và tổng TiềnTT vào view
            ViewBag.CustomerCount = customerCount;
            ViewBag.TotalPaidInvoicesCount = totalPaidInvoicesCount;
            ViewBag.TotalAmountPaid = totalAmountPaid;
            ViewBag.UnreadNotificationCount = unreadNotificationCount;
            return View();
        }
            else
            {
                return Redirect("/Account/Login");
            }
        }


    }

}
