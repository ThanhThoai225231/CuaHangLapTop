using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.ViewComponents
{
    public class TopBanChayViewComponent : ViewComponent
    {
        private readonly QuanLyNhaHangContext _context;

        public TopBanChayViewComponent(QuanLyNhaHangContext context)
        {
            _context = context;
        }
		public IViewComponentResult Invoke()
		{
			var bestSellingProducts = _context.MonAns.Where(m => m.TrangThaiMA == 1)
				.OrderByDescending(item => item.SoLuongDaBan)
				.ThenBy(item => item.MaMon) // Sắp xếp theo Mã sản phẩm tăng dần nếu có cùng SoLuongDaBan
				.Take(4)
				.ToList();

			return View(bestSellingProducts);
		}
	}
}
