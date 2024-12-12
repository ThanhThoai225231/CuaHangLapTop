using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models; // Đảm bảo rằng bạn đã sử dụng namespace chứa các mô hình
using X.PagedList;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using NuGet.Configuration;
using System.Data;
using Microsoft.AspNetCore.Identity;


namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLHController : Controller
    {
        private readonly QuanLyNhaHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminLHController(QuanLyNhaHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin" || HttpContext.Session.GetString("VaiTro") == "LeTan" || HttpContext.Session.GetString("VaiTro") == "ThuNgan"))
            {



                IQueryable<LoHangViewModel> query = _context.LoHangs
    .Where(lh => lh.TrangThaiLH == 2)
    .Join(
        _context.ChiTietLHs,
        lh => lh.MaLH,
        cth => cth.MaLH,
        (lh, cth) => new { LoHang = lh, ChiTietLH = cth })
    .Join(
        _context.MonAns,
        combined => combined.ChiTietLH.MaMon,
        sp => sp.MaMon,
        (combined, sp) => new LoHangViewModel
        {
            MaLH = combined.LoHang.MaLH,
            NgayNhap = combined.LoHang.NgayNhap,
            GiaLo = combined.LoHang.GiaLo,
            TrangThaiLH = combined.LoHang.TrangThaiLH,
            GiaNhap = combined.ChiTietLH.GiaNhap,
            SoLuong = combined.ChiTietLH.SoLuong,
            DaBan = combined.ChiTietLH.DaBan,
            TenSanPham = sp.TenMon // Gán tên sản phẩm từ bảng SanPham vào TenSanPham của LoHangViewModel
        });

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(vm =>
                        vm.MaLH.ToString().Contains(search) ||
                        vm.TenSanPham.Contains(search));
                }

                if (startDate.HasValue || endDate.HasValue)
                {
                    if (startDate.HasValue && !endDate.HasValue)
                    {
                        query = query.Where(d => d.NgayNhap.Date >= startDate.Value.Date && d.NgayNhap.Date <= DateTime.Now.Date);
                    }
                    else if (!startDate.HasValue && endDate.HasValue)
                    {
                        query = query.Where(d => d.NgayNhap.Date <= endDate.Value.Date);
                    }
                    else if (startDate.HasValue && endDate.HasValue)
                    {
                        if (startDate.Value.Date == endDate.Value.Date)
                        {
                            query = query.Where(d => d.NgayNhap.Date == startDate.Value.Date);
                        }
                        else
                        {
                            query = query.Where(d => d.NgayNhap.Date >= startDate.Value.Date && d.NgayNhap.Date <= endDate.Value.Date);
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
            else
            {
                return Redirect("/Account/Login");
            }


        }

    }
}