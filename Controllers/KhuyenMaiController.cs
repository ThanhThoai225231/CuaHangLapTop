using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Controllers
{
    public class KhuyenMaiController : Controller
    {

		private readonly QuanLyNhaHangContext _context;

		public KhuyenMaiController(QuanLyNhaHangContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
			string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

			if (tenDangNhap == null)
			{
				return View();
			}

			var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
			int maKhachHang = khachHang.MaKH;

			var query = _context.KhuyenMais.Where(d => d.MaKH == maKhachHang && d.NgayKT >= DateTime.Now).ToList();
			return View(query);
        }
    }
}
