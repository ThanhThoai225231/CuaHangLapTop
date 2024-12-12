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
    public class TKController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public TKController(QuanLyNhaHangContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public record DailyRevenueDTO(DateTime Date, double DoanhThu, double LoiNhuan);
        public record MonthlyRevenueDTO(DateTime Month, double DoanhThu, double LoiNhuan);

        public record YearlyRevenueDTO(int year, double DoanhThu, double LoiNhuan);
        [HttpGet]
        public IActionResult ThongKeDoanhThu(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = new List<DailyRevenueDTO>();

                for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
                {
                    // Tính tổng giá trị sản phẩm trong đơn hàng
                    var totalProductValue = _context.HoaDons
                        .Where(o => o.TrangThaiHD == 2 && o.NgayHd.Date == date.Date)
                        .SelectMany(o => o.ChiTietHds)
                        .Join(_context.MonAns, od => od.MaMon, p => p.MaMon, (od, p) => new { od, p })
                        .Sum(x => (double?)x.od.SoLuongCt * x.p.GiaGoc) ?? 0;

                    // Tính tổng giá trị đơn hàng (DoanhThu)
                    var totalAmount = _context.HoaDons
                        .Where(o => o.TrangThaiHD == 2 && o.NgayHd.Date == date.Date)
                        .Sum(o => (double?)o.TongTien) ?? 0;

                    // Tính lợi nhuận
                    var profit = totalAmount - totalProductValue;

                    var dailyResult = new DailyRevenueDTO(date, totalAmount, profit);

                    result.Add(dailyResult);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { Error = "Lỗi trong quá trình xử lý yêu cầu: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ThongKeDoanhThuThang(DateTime startMonth, DateTime endMonth)
        {
            try
            {
                var result = new List<MonthlyRevenueDTO>();

                // Lặp qua từng tháng từ startMonth đến endMonth
                for (DateTime month = startMonth.Date; month <= endMonth.Date; month = month.AddMonths(1))
                {
                    // Tính ngày đầu tiên và ngày cuối cùng của tháng
                    var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
                    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                    // Tính tổng giá trị sản phẩm trong đơn hàng
                    var totalProductValue = _context.HoaDons
                        .Where(o => o.TrangThaiHD == 2 && o.NgayHd.Date >= firstDayOfMonth && o.NgayHd.Date <= lastDayOfMonth)
                        .SelectMany(o => o.ChiTietHds)
                        .Join(_context.MonAns, od => od.MaMon, p => p.MaMon, (od, p) => new { od, p })
                        .Sum(x => (double?)x.od.SoLuongCt * x.p.GiaGoc) ?? 0;

                    // Tính tổng giá trị đơn hàng (DoanhThu)
                    var totalAmount = _context.HoaDons
                        .Where(o => o.TrangThaiHD == 2 && o.NgayHd.Date >= firstDayOfMonth && o.NgayHd.Date <= lastDayOfMonth)
                        .Sum(o => (double?)o.TongTien) ?? 0;

                    // Tính lợi nhuận
                    var profit = totalAmount - totalProductValue;

                    var monthlyResult = new MonthlyRevenueDTO(firstDayOfMonth, totalAmount, profit);

                    result.Add(monthlyResult);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { Error = "Lỗi trong quá trình xử lý yêu cầu: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ThongKeDoanhThuNam(int startYear, int endYear)
        {
            try
            {
                var result = new List<YearlyRevenueDTO>();

                for (int year = startYear; year <= endYear; year++)
                {
                    // Tính tổng doanh thu và lợi nhuận cho từng năm
                    var totalRevenue = _context.HoaDons
                        .Where(o => o.TrangThaiHD == 3 && o.NgayHd.Year == year)
                        .Sum(o => (double?)o.TongTien) ?? 0;

                    var totalProductValue = _context.HoaDons
                        .Where(o => o.TrangThaiHD == 2 && o.NgayHd.Year == year)
                        .SelectMany(o => o.ChiTietHds)
                        .Join(_context.MonAns, od => od.MaMon, p => p.MaMon, (od, p) => new { od, p })
                        .Sum(x => (double?)x.od.SoLuongCt * x.p.GiaGoc) ?? 0;

                    var totalProfit = totalRevenue - totalProductValue;

                    var yearlyResult = new YearlyRevenueDTO(year, totalRevenue, totalProfit);

                    result.Add(yearlyResult);
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { Error = "Lỗi trong quá trình xử lý yêu cầu: " + ex.Message });
            }
        }
    }
}
