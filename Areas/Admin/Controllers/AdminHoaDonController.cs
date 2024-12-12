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
    public class AdminHoaDonController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public AdminHoaDonController(QuanLyNhaHangContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {

            var query = _context.HoaDons.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.MaHd.ToString().Contains(search) ||                 
                    d.NgayHd.ToString().Contains(search) ||
                    d.HinhThucTT.Contains(search) ||
                    d.TongTien.ToString().Contains(search) ||
                    d.TienGiam.ToString().Contains(search) ||
                    d.TienTt.ToString().Contains(search) ||                
                    d.ChiTietHds.Any(ct =>
                        ct.MaMon.ToString().Contains(search) ||              
                        ct.TenMon.Contains(search) ||                         
                        ct.SoLuongCt.ToString().Contains(search) ||
                        ct.ThanhTien.ToString().Contains(search)));
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayHd.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= endDate.Value.Date);
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

            return View(pagedList);

        }

        public IActionResult ChoXacNhan(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {

            var query = _context.HoaDons.Where(d => d.TrangThaiHD == 1).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                     d.MaHd.ToString().Contains(search) ||
                     d.NgayHd.ToString().Contains(search) ||
                     d.HinhThucTT.Contains(search) ||
                     d.TongTien.ToString().Contains(search) ||
                     d.TienGiam.ToString().Contains(search) ||
                     d.TienTt.ToString().Contains(search) ||
                     d.ChiTietHds.Any(ct =>
                         ct.MaMon.ToString().Contains(search) ||
                         ct.TenMon.Contains(search) ||
                         ct.SoLuongCt.ToString().Contains(search) ||
                         ct.ThanhTien.ToString().Contains(search)));
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayHd.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= endDate.Value.Date);
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

            return View(pagedList);
        }

        public IActionResult DaThanhToan(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {

            var query = _context.HoaDons.Where(d => d.TrangThaiHD == 2).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                     d.MaHd.ToString().Contains(search) ||
                     d.NgayHd.ToString().Contains(search) ||
                     d.HinhThucTT.Contains(search) ||
                     d.TongTien.ToString().Contains(search) ||
                     d.TienGiam.ToString().Contains(search) ||
                     d.TienTt.ToString().Contains(search) ||
                     d.ChiTietHds.Any(ct =>
                         ct.MaMon.ToString().Contains(search) ||
                         ct.TenMon.Contains(search) ||
                         ct.SoLuongCt.ToString().Contains(search) ||
                         ct.ThanhTien.ToString().Contains(search)));
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayHd.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= endDate.Value.Date);
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

            return View(pagedList);
        }

        public IActionResult Huy(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {

            var query = _context.HoaDons.Where(d => d.TrangThaiHD == 3).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                     d.MaHd.ToString().Contains(search) ||
                     d.NgayHd.ToString().Contains(search) ||
                     d.HinhThucTT.Contains(search) ||
                     d.TongTien.ToString().Contains(search) ||
                     d.TienGiam.ToString().Contains(search) ||
                     d.TienTt.ToString().Contains(search) ||
                     d.ChiTietHds.Any(ct =>
                         ct.MaMon.ToString().Contains(search) ||
                         ct.TenMon.Contains(search) ||
                         ct.SoLuongCt.ToString().Contains(search) ||
                         ct.ThanhTien.ToString().Contains(search)));
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayHd.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayHd.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayHd.Date >= startDate.Value.Date && d.NgayHd.Date <= endDate.Value.Date);
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

            return View(pagedList);
        }


        public IActionResult Details(int id)
        {

            var donHang = _context.DonHangs.FirstOrDefault(dh => dh.MaDonHang == id);
            var chiTietDonHang = _context.ChiTietDhs.Where(ctdh => ctdh.MaDonHang == id).ToList();
            int maHoaDon = donHang.MaHd;
            var hoaDon = _context.HoaDons.FirstOrDefault(hd => hd.MaHd == maHoaDon);
            var chiTietHoaDon = _context.ChiTietHds.Where(cthd => cthd.MaHd == maHoaDon).ToList();
            var khachHang = _context.KhachHangs.FirstOrDefault(kh => kh.MaKH == donHang.MaKH);
            if (donHang == null)
            {
                return View("Error");
            }

            var ChiTietDonHang = new List<ChiTietDh>();

            var hoaDonViewModel = new HoaDonViewModel
            {
                MaHd = donHang.MaHd,
                MaDonHang = donHang.MaDonHang,
                NgayDatHang = donHang.NgayDatHang,
                TrangThaiDh = donHang.TrangThaiDh,
                HinhThucTT = hoaDon.HinhThucTT,
                MaKH = donHang.MaKH,               
                TenKh = khachHang.TenKh,
                Sdtkh = khachHang.Sdtkh,
                EmailKh = khachHang.EmailKh,
                GioiTinhKh = khachHang.GioiTinhKh,
                DiaChiKh = khachHang.DiaChiKh,               
                ChiTietDonHang = chiTietDonHang,
                ChiTietHoaDon = chiTietHoaDon,
                TongTien = (float)hoaDon.TongTien,
                TienGiam = (float)hoaDon.TienGiam,
                TienTt = (float)hoaDon.TienTt,

            };

            return View(hoaDonViewModel);
        }

        [HttpPost]
        public IActionResult UpdateTrangThai(int maHoaDon, int newTrangThai)
        {
            var donHang = _context.HoaDons.FirstOrDefault(d => d.MaHd == maHoaDon);
            if (donHang != null)
            {
                donHang.TrangThaiHD = newTrangThai;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
