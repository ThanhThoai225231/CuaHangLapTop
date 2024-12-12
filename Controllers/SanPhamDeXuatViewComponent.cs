using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.ViewComponents
{
    public class SanPhamDeXuatViewComponent : ViewComponent
    {
        private readonly QuanLyNhaHangContext _context;

        public SanPhamDeXuatViewComponent(QuanLyNhaHangContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            // Truy vấn bảng HanhViND để đếm số lần mỗi MaMon xuất hiện cho TenDangNhap đó
            var topMonAnIds = _context.HanhViNDs
                .Where(h => h.TenDangNhap == tenDangNhap)
                .GroupBy(h => h.MaMon)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key) // Sắp xếp theo MaMon tăng dần nếu có cùng số lượng
                .Take(4)
                .Select(g => g.Key)
                .ToList();

            // Truy vấn bảng MonAns để lấy thông tin các món ăn theo danh sách MaMon đã lấy được
            var topMonAns = _context.MonAns
                .Where(m => topMonAnIds.Contains(m.MaMon))
                .ToList();

            return View(topMonAns);
        }

    }
}
