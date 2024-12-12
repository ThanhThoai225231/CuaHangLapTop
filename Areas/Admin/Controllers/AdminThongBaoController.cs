using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminThongBaoController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public AdminThongBaoController(QuanLyNhaHangContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin" || HttpContext.Session.GetString("VaiTro") == "LeTan" || HttpContext.Session.GetString("VaiTro") == "ThuNgan"))
            {

                var query = _context.ThongBaos
     .OrderByDescending(tb => tb.ThoiGian)
     .AsQueryable();

                int unreadNotificationCount = _context.ThongBaos.Count(tb => tb.TrangThaiTB == 1);

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(d =>
                         d.MaTB.ToString().Contains(search) ||
                         d.NoiDung.Contains(search) ||
                         d.MaHD.ToString().Contains(search) ||
                         d.ThoiGian.ToString().Contains(search));

                }

                if (startDate.HasValue || endDate.HasValue)
                {
                    if (startDate.HasValue && !endDate.HasValue)
                    {
                        query = query.Where(d => d.ThoiGian.Date >= startDate.Value.Date && d.ThoiGian.Date <= DateTime.Now.Date);
                    }
                    else if (!startDate.HasValue && endDate.HasValue)
                    {
                        query = query.Where(d => d.ThoiGian.Date <= endDate.Value.Date);
                    }
                    else if (startDate.HasValue && endDate.HasValue)
                    {
                        if (startDate.Value.Date == endDate.Value.Date)
                        {
                            query = query.Where(d => d.ThoiGian.Date == startDate.Value.Date);
                        }
                        else
                        {
                            query = query.Where(d => d.ThoiGian.Date >= startDate.Value.Date && d.ThoiGian.Date <= endDate.Value.Date);
                        }
                    }
                }


                int pageNumber = page ?? 1;
                var pagedList = query.ToPagedList(pageNumber, pageSize);


                int startItem = (pageNumber - 1) * pageSize + 1;
                int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

                int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
                int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
                int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);

                ViewBag.Search = search;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;

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
                ViewBag.UnreadNotificationCount = unreadNotificationCount;

                return View(pagedList);

            }
            else
            {
                return Redirect("/Account/Login");
            }

        }

        [HttpPost]
        public IActionResult MarkAsRead(int notificationId)
        {
            var notification = _context.ThongBaos.FirstOrDefault(tb => tb.MaTB == notificationId);

            // Kiểm tra xem thông báo có tồn tại hay không
            if (notification != null)
            {
                // Cập nhật trạng thái TrangThaiTB từ 1 sang 2
                notification.TrangThaiTB = 2;

                // Lưu thay đổi vào CSDL
                _context.SaveChanges();

              
            }
            else
            {
                // Trả về kết quả nếu không tìm thấy thông báo
                return Json(new { success = false, message = "Không tìm thấy thông báo" });
            }

            return Redirect("/AdminThongBao");
        }

    }

}
