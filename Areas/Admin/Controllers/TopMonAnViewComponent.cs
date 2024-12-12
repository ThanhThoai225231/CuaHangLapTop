using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Areas.Admin.ViewComponents
{
   
    public class TopMonAnViewComponent : ViewComponent
    {
        private readonly QuanLyNhaHangContext _context;

        public TopMonAnViewComponent(QuanLyNhaHangContext context)
        {
            _context = context;
        }
		public IViewComponentResult Invoke()
		{
			var bestSellingProducts = _context.MonAns
				.OrderByDescending(item => item.SoLuongDaBan)
				.ThenBy(item => item.MaMon) // Sắp xếp theo Mã sản phẩm tăng dần nếu có cùng SoLuongDaBan
				.Take(4)
				.ToList();

			return View(bestSellingProducts);
		}

	}
}
