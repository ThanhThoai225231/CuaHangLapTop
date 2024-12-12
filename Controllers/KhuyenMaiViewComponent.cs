using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.ViewComponents
{

	public class KhuyenMaiViewComponent : ViewComponent
	{
		private readonly QuanLyNhaHangContext _context;

		public KhuyenMaiViewComponent(QuanLyNhaHangContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var latestOrders = _context.MonAns
			   .OrderByDescending(order => order.GiaKhuyenMai < order.GiaBan)
			   .ThenBy(item => item.MaMon)
			   .Take(4)
			   .ToList();

			return View(latestOrders);
		}

	}
}
