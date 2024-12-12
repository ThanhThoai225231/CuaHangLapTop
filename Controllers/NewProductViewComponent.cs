using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.ViewComponents
{

	public class NewProductViewComponent : ViewComponent
	{
		private readonly QuanLyNhaHangContext _context;

		public NewProductViewComponent(QuanLyNhaHangContext context)
		{
			_context = context;
		}
		public IViewComponentResult Invoke()
		{
			var latestOrders = _context.MonAns.Where(m => m.TrangThaiMA == 1)
			   .OrderByDescending(order => order.NgayMoBan)
               .ThenBy(item => item.MaMon)
               .Take(4)
			   .ToList();

			return View(latestOrders);
		}

	}
}
