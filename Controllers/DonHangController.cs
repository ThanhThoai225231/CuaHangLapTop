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

namespace ThanhThoaiRestaurant.Controllers
{
    public class DonHangController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public DonHangController(QuanLyNhaHangContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            if (tenDangNhap == null)
            {
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            int maKhachHang = khachHang.MaKH;

            var query = _context.DonHangs.Where(d => d.MaKH == maKhachHang);

            // Áp dụng tìm kiếm theo số đơn hàng
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.MaDonHang.ToString().Contains(search) ||                  
                    d.MaHd.ToString().Contains(search) ||
                    d.NguoiNhan.Contains(search) ||
                    d.SDTNN.Contains(search) ||
                    d.DiaChiNhan.Contains(search) ||
                    d.GhiChu.Contains(search) ||
                    d.ChiTietDhs.Any(ct =>
                        ct.MaMon.ToString().Contains(search) ||              
                        ct.TenMonAnDh.Contains(search) ||                         
                        ct.SoLuongMmdh.ToString().Contains(search)));
                
            }
            // Áp dụng tìm kiếm theo khoảng thời gian
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayDatHang.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= endDate.Value.Date);
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

        public IActionResult ChoXacNhan(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 1)
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            if (tenDangNhap == null)
            {
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            int maKhachHang = khachHang.MaKH;

            var query = _context.DonHangs.Where(d => d.MaKH == maKhachHang && d.TrangThaiDh == 1);

            // Áp dụng tìm kiếm theo số đơn hàng
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                     d.MaDonHang.ToString().Contains(search) ||
                     d.MaHd.ToString().Contains(search) ||
                     d.NguoiNhan.Contains(search) ||
                     d.SDTNN.Contains(search) ||
                     d.DiaChiNhan.Contains(search) ||
                     d.GhiChu.Contains(search) ||
                     d.ChiTietDhs.Any(ct =>
                         ct.MaMon.ToString().Contains(search) ||
                         ct.TenMonAnDh.Contains(search) ||
                         ct.SoLuongMmdh.ToString().Contains(search)));

            }
            // Áp dụng tìm kiếm theo khoảng thời gian
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayDatHang.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= endDate.Value.Date);
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

        public IActionResult DaGiao(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 1)
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            if (tenDangNhap == null)
            {
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            int maKhachHang = khachHang.MaKH;

            var query = _context.DonHangs.Where(d => d.MaKH == maKhachHang && d.TrangThaiDh == 3);

            // Áp dụng tìm kiếm theo số đơn hàng
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                     d.MaDonHang.ToString().Contains(search) ||
                     d.MaHd.ToString().Contains(search) ||
                     d.NguoiNhan.Contains(search) ||
                     d.SDTNN.Contains(search) ||
                     d.DiaChiNhan.Contains(search) ||
                     d.GhiChu.Contains(search) ||
                     d.ChiTietDhs.Any(ct =>
                         ct.MaMon.ToString().Contains(search) ||
                         ct.TenMonAnDh.Contains(search) ||
                         ct.SoLuongMmdh.ToString().Contains(search)));

            }
            // Áp dụng tìm kiếm theo khoảng thời gian
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayDatHang.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= endDate.Value.Date);
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

        public IActionResult Huy(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 1)
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            if (tenDangNhap == null)
            {
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            int maKhachHang = khachHang.MaKH;

            var query = _context.DonHangs.Where(d => d.MaKH == maKhachHang && d.TrangThaiDh == 4);

            // Áp dụng tìm kiếm theo số đơn hàng
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.MaDonHang.ToString().Contains(search) ||
                    d.MaHd.ToString().Contains(search) ||
                    d.NguoiNhan.Contains(search) ||
                    d.SDTNN.Contains(search) ||
                    d.DiaChiNhan.Contains(search) ||
                    d.GhiChu.Contains(search) ||
                    d.ChiTietDhs.Any(ct =>
                        ct.MaMon.ToString().Contains(search) ||
                        ct.TenMonAnDh.Contains(search) ||
                        ct.SoLuongMmdh.ToString().Contains(search)));

            }
            // Áp dụng tìm kiếm theo khoảng thời gian
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayDatHang.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= endDate.Value.Date);
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

        public IActionResult DangGiao(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 1)
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");

            if (tenDangNhap == null)
            {
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            int maKhachHang = khachHang.MaKH;

            var query = _context.DonHangs.Where(d => d.MaKH == maKhachHang && d.TrangThaiDh == 2);

            // Áp dụng tìm kiếm theo số đơn hàng
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                    d.MaDonHang.ToString().Contains(search) ||
                    d.MaHd.ToString().Contains(search) ||
                    d.NguoiNhan.Contains(search) ||
                    d.SDTNN.Contains(search) ||
                    d.DiaChiNhan.Contains(search) ||
                    d.GhiChu.Contains(search) ||
                    d.ChiTietDhs.Any(ct =>
                        ct.MaMon.ToString().Contains(search) ||
                        ct.TenMonAnDh.Contains(search) ||
                        ct.SoLuongMmdh.ToString().Contains(search)));

            }
            // Áp dụng tìm kiếm theo khoảng thời gian
            if (startDate.HasValue || endDate.HasValue)
            {
                if (startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= DateTime.Now.Date);
                }
                else if (!startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(d => d.NgayDatHang.Date <= endDate.Value.Date);
                }
                else if (startDate.HasValue && endDate.HasValue)
                {
                    if (startDate.Value.Date == endDate.Value.Date)
                    {
                        query = query.Where(d => d.NgayDatHang.Date == startDate.Value.Date);
                    }
                    else
                    {
                        query = query.Where(d => d.NgayDatHang.Date >= startDate.Value.Date && d.NgayDatHang.Date <= endDate.Value.Date);
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
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap"); // Lấy tên đăng nhập từ session
            if (tenDangNhap == null)
            {
                // Xử lý trường hợp người dùng chưa đăng nhập
                return View("Error");
            }

            var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            if (khachHang == null)
            {
                // Xử lý trường hợp không tìm thấy thông tin khách hàng
                return View("Error");
            }

                var donHang = _context.DonHangs
               
                .FirstOrDefault(dh => dh.MaDonHang == id);
                var chiTietDonHang = _context.ChiTietDhs.Where(ctdh => ctdh.MaDonHang == id).ToList();
                int maHoaDon = donHang.MaHd;
                var hoaDon = _context.HoaDons.FirstOrDefault(hd => hd.MaHd == maHoaDon);
                var chiTietHoaDon = _context.ChiTietHds
                .Where(cthd => cthd.MaHd == maHoaDon)
                .ToList();
               
                if (donHang == null)
                {
                    return View("Error");
                }

                var ChiTietDonHang = new List<ChiTietDh>();

            var hoaDonViewModel = new HoaDonViewModel
            {
                TenKh = donHang.NguoiNhan,
                Sdtkh = donHang.SDTNN,
                EmailKh = khachHang.EmailKh,
                GioiTinhKh = khachHang.GioiTinhKh,
                DiaChiNhan = donHang.DiaChiNhan,
                MaDonHang = donHang.MaDonHang,
                NgayDatHang = donHang.NgayDatHang,
                TrangThaiDh = donHang.TrangThaiDh,
                HinhThucTT = hoaDon.HinhThucTT,
                SDTNN = donHang.SDTNN,
                NguoiNhan = donHang.NguoiNhan,
                
                GhiChu = donHang.GhiChu,
                    MaKH = donHang.MaKH,
                    MaHd = donHang.MaHd,
                    ChiTietDonHang = chiTietDonHang,
                    ChiTietHoaDon = chiTietHoaDon,
                    TongTien = (float)hoaDon.TongTien,
                    TienGiam = (float)hoaDon.TienGiam,
                    TienTt = (float)hoaDon.TienTt,
                   
                };

                return View(hoaDonViewModel);
            }


        [HttpPost]
        public IActionResult UpdateTrangThai(int maDonHang, int newTrangThai)
        {
            var donHang = _context.DonHangs.FirstOrDefault(d => d.MaDonHang == maDonHang);
            if (donHang != null)
            {
                donHang.TrangThaiDh = newTrangThai;
                _context.SaveChanges();

                var thongbao = new ThongBao
                {
                    MaTB = maDonHang,
                    NoiDung = $"Vừa có đơn hàng bị khách hàng huỷ - Mã đơn hàng: {maDonHang}",
                    MaHD = maDonHang,
                    ThoiGian = DateTime.Now,
                    TrangThaiTB = 1

                };
                _context.ThongBaos.Add(thongbao);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


    }
    

    }
