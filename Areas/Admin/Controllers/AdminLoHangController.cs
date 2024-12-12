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
    public class AdminLoHangController : Controller
    {
        private readonly QuanLyNhaHangContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminLoHangController(QuanLyNhaHangContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int? page, string search, DateTime? startDate, DateTime? endDate, int pageSize = 3)
        {
            if (HttpContext.Session.GetString("TenDangNhap") != null && (HttpContext.Session.GetString("VaiTro") == "Admin" || HttpContext.Session.GetString("VaiTro") == "LeTan" || HttpContext.Session.GetString("VaiTro") == "ThuNgan"))
            {



                IQueryable<LoHangViewModel> query = _context.LoHangs
    .Where(lh => lh.TrangThaiLH == 1)
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

        public IActionResult NhapKho(int maMon, double giaLo)
        {
            var model = new LoHangViewModel
            {
                MaMon = maMon,
                GiaLo = giaLo,
                MaLH = GenerateUniqueMaLH(),

            };
            return View(model);
        }

        // POST: AdminLoHang/NhapKho
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NhapKho(LoHangViewModel model)
        {


            if (ModelState.IsValid)
            {
                
                var loHang = new LoHang
                {
                    MaLH = model.MaLH,
                    NgayNhap = DateTime.Now,
                    GiaLo = (model.GiaNhap * model.SoLuong),
                    TrangThaiLH = 1,
                    MaNPP = 1
                };             
                 _context.LoHangs.Add(loHang);
                 _context.SaveChanges();

                var product = _context.MonAns.SingleOrDefault(m => m.MaMon == model.MaMon);

                var chiTietLH = new ChiTietLH
                {
                    MaLH = loHang.MaLH,
                    MaMon = model.MaMon,
                    SoLuong = model.SoLuong,
                    DaBan = 0,
                    GiaNhap = (model.GiaNhap),
                    HinhAnh = product.HinhAnh,
                    NgayNhapLH = DateTime.Now
                    
                };
                _context.ChiTietLHs.Add(chiTietLH);
                _context.SaveChanges();

                if (product != null)
                {
                    // Cập nhật số lượng sản phẩm
                    product.SoLuong += model.SoLuong;
                    _context.Update(product);
                    _context.SaveChanges();
                }

                    return RedirectToAction("Index", "AdminLoHang"); // Redirect to an appropriate page after saving
            }
            return View(model);
        }

        public bool IsMaLHExists(int maLH)
        {
            return _context.LoHangs.Any(lh => lh.MaLH == maLH);
        }

        public int GenerateUniqueMaLH()
        {
            Random random = new Random();
            int maLH;
            do
            {
                maLH = random.Next(1, 1000000); // Sinh mã lô hàng ngẫu nhiên từ 1 đến 999999
            } while (IsMaLHExists(maLH)); // Kiểm tra mã lô hàng đã tồn tại trong CSDL chưa

            return maLH;
        }

        public IActionResult Details(int id)
        {

            var donHang = _context.LoHangs.FirstOrDefault(dh => dh.MaLH == id);
            var chiTietLoHang = _context.ChiTietLHs.FirstOrDefault(dh => dh.MaLH == id);
            var sanPham = _context.MonAns.FirstOrDefault(sp => sp.MaMon == chiTietLoHang.MaMon);

            if (donHang == null)
            {
                return View("Error");
            }

            

            var hoaDonViewModel = new LoHangViewModel
            {
                MaLH = donHang.MaLH,
                NgayNhap = donHang.NgayNhap,
                TenSanPham = sanPham.TenMon,
                HinhAnh = chiTietLoHang.HinhAnh,
                GiaNhap = chiTietLoHang.GiaNhap,
                SoLuong = chiTietLoHang.SoLuong,
                DaBan = chiTietLoHang.DaBan,


            };

            return View(hoaDonViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> XuLy(int id)
        {
            var loHangToUpdate = await _context.LoHangs
                .FirstOrDefaultAsync(lh => lh.MaLH == id);

            if (loHangToUpdate != null)
            {
                loHangToUpdate.TrangThaiLH = 2;
                _context.Update(loHangToUpdate); // Sử dụng Update thay vì Add
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "AdminLoHang");
        }


    }
}
