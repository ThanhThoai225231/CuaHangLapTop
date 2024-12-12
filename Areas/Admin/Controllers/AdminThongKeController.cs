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
    
    public class AdminThongKeController : Controller
    {


        private readonly QuanLyNhaHangContext _context;

        public AdminThongKeController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin" || HttpContext.Session.GetString("VaiTro") == "ThuNgan"))
            {
                return View();
            }
            else
            {
                return Redirect("/Account/Login");
            }  
        }

        [HttpGet]
        public IActionResult ThongKeDoanhThu(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dailyRevenues = new Dictionary<DateTime, double>();

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    var dailyRevenue = _context.HoaDons
                        .Where(hd => hd.TrangThaiHD == 2 && hd.NgayHd.Date == date.Date)
                        .Sum(hd => hd.TongTien);

                    dailyRevenues.Add(date, dailyRevenue);
                }

                return Ok(dailyRevenues);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi trong quá trình xử lý yêu cầu: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ThongKeDoanhThuThang(DateTime startMonth, DateTime endMonth)
        {
            try
            {
                var monthlyRevenues = new Dictionary<string, double>();

                for (var date = startMonth; date <= endMonth; date = date.AddMonths(1))
                {
                    var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    var monthlyRevenue = _context.HoaDons
                        .Where(hd => hd.TrangThaiHD == 2 && hd.NgayHd >= firstDayOfMonth && hd.NgayHd <= lastDayOfMonth)
                        .Sum(hd => hd.TongTien);

                    monthlyRevenues.Add(date.ToString("MM/yyyy"), monthlyRevenue);
                }

                return Ok(monthlyRevenues);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi trong quá trình xử lý yêu cầu: " + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ThongKeDoanhThuNam(int startYear, int endYear)
        {
            try
            {
                var yearlyRevenues = new Dictionary<int, double>();

                for (var year = startYear; year <= endYear; year++)
                {
                    var yearlyRevenue = _context.HoaDons
                        .Where(hd => hd.TrangThaiHD == 2 && hd.NgayHd.Year == year)
                        .Sum(hd => hd.TongTien);

                    yearlyRevenues.Add(year, yearlyRevenue);
                }

                return Ok(yearlyRevenues);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi trong quá trình xử lý yêu cầu: " + ex.Message);
            }
        }

    }
}
