using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Areas.Admin.ViewComponents
{

    public class TopMonAnPhoBienViewComponent : ViewComponent
    {
        private readonly QuanLyNhaHangContext _context;

        public TopMonAnPhoBienViewComponent(QuanLyNhaHangContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var latestOrders = _context.DonHangs
                .OrderByDescending(order => order.NgayDatHang)
                .Take(5)
                .ToList();

            return View(latestOrders);
        }

    }
}
