using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using static ThanhThoaiRestaurant.Models.GioHang;
using System.Text.Json;

namespace ThanhThoaiRestaurant.Controllers
{
    public class GioHangController : Controller
    {
        public QuanLyNhaHangContext _context;

        public GioHangController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var gioHangJson = HttpContext.Session.GetString("GioHang");
            List<ChiTietGh> chiTietGhs = new List<ChiTietGh>();

            if (!string.IsNullOrEmpty(gioHangJson))
            {
                chiTietGhs = JsonSerializer.Deserialize<List<ChiTietGh>>(gioHangJson);
                // Lưu các giá trị vào Session
                
            }

           
            // Truyền Model chứa giỏ hàng đến View
            return View(chiTietGhs);
        }

        private float CalculateTotalAmount(List<ChiTietGh> chiTietGhs)
        {
            float totalAmount = 0;
            foreach (var chiTietGh in chiTietGhs)
            {
                // Thực hiện tính toán tổng tiền cho mỗi món trong chi tiết giỏ hàng
                totalAmount += chiTietGh.ThanhTien;
            }
            return totalAmount;
        }

        [HttpPost]
        public IActionResult ThemVaoGioHang(int maMon, int soLuongMM)
        {
            var isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");

            if (string.IsNullOrEmpty(isLoggedIn) || isLoggedIn != "true")
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng họ đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }
            else
            {
                // Lấy món ăn từ CSDL dựa trên mã món
                var monAn = _context.MonAns.FirstOrDefault(m => m.MaMon == maMon);

                if (monAn == null)
                {
                    return Json(new { success = false, message = "Món ăn không tồn tại." });
                }

                // Lấy giỏ hàng từ Session
                var gioHangJson = HttpContext.Session.GetString("GioHang");
                List<ChiTietGh> chiTietGhs;

                if (gioHangJson != null)
                {
                    // Nếu giỏ hàng đã tồn tại trong Session, lấy nó từ Session
                    chiTietGhs = JsonSerializer.Deserialize<List<ChiTietGh>>(gioHangJson);
                }
                else
                {
                    // Nếu giỏ hàng chưa tồn tại trong Session, khởi tạo nó
                    chiTietGhs = new List<ChiTietGh>();
                }

                // Kiểm tra xem món ăn đã có trong giỏ hàng chưa
                var existingItem = chiTietGhs.FirstOrDefault(item => item.MaMon == maMon);

                if (existingItem != null)
                {
                    // Nếu món ăn đã tồn tại trong giỏ hàng, cập nhật số lượng
                    existingItem.SoLuongMM += soLuongMM;
                }
                else
                {
                    // Nếu món ăn chưa tồn tại trong giỏ hàng, thêm mới
                    chiTietGhs.Add(new ChiTietGh
                    {
                        MaMon = maMon,
                        SoLuongMM = soLuongMM,
                        TenMon = monAn.TenMon, // Lấy tên món ăn từ CSDL
                        HinhAnh = monAn.HinhAnh, // Lấy hình ảnh từ CSDL
                        GiaBan = (float)monAn.GiaBan,
                        ThanhTien = (float)(monAn.GiaBan * soLuongMM),
                        SoLuong = monAn.SoLuong,
                        MaMonNavigation = monAn

                    });
                }


                var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
           /*     if (!string.IsNullOrEmpty(tenDangNhap))
                {
                    var hanhViND = new HanhViND
                    {
                        Id = GenerateRandomId(),
                        TenDangNhap = tenDangNhap,
                        MaMon = monAn.MaMon,
                        ThoiGian = DateTime.Now,
                        HanhDong = 2 // Ví dụ: 2  đại diện cho hành động "Thêm vào giỏ hàng"
                    };

                    _context.HanhViNDs.Add(hanhViND);
                    _context.SaveChanges();
                } */

                // Lưu giỏ hàng vào Session
                HttpContext.Session.SetString("GioHang", JsonSerializer.Serialize(chiTietGhs));

                return RedirectToAction("Index", "Product");
            }
        }

        [HttpPost]
        public IActionResult Remove_1_FromGioHang(int maSanPham)
        {
            var gioHangJson = HttpContext.Session.GetString("GioHang");

            List<ChiTietGh> chiTietGhs;

            if (gioHangJson != null)
            {
                // Nếu giỏ hàng đã tồn tại trong Session, lấy nó từ Session
                chiTietGhs = JsonSerializer.Deserialize<List<ChiTietGh>>(gioHangJson);
            }
            else
            {
                // Nếu giỏ hàng chưa tồn tại trong Session, khởi tạo nó
                chiTietGhs = new List<ChiTietGh>();
            }
            // Tìm kiếm sản phẩm trong giỏ hàng
            var existingItem = chiTietGhs.FirstOrDefault(item => item.MaMon == maSanPham);

            if (existingItem != null)
            {
                // If the item exists in the cart, update the quantity
                existingItem.SoLuongMM -= 1;

                // If the quantity is zero, remove the item from the cart
                if (existingItem.SoLuongMM <= 0)
                {
                    chiTietGhs.Remove(existingItem);
                }

                existingItem.ThanhTien = (float)(existingItem.SoLuongMM * existingItem.GiaBan);
            }

            // Save the updated cart back to the session
            HttpContext.Session.SetString("GioHang", JsonSerializer.Serialize(chiTietGhs));

            return RedirectToAction("Index", "GioHang");
        }

        [HttpPost]
        public IActionResult XoaMonAn(int maMon)
        {
            // Lấy giỏ hàng từ Session
            var gioHangJson = HttpContext.Session.GetString("GioHang");
            List<ChiTietGh> chiTietGhs = new List<ChiTietGh>();

            if (!string.IsNullOrEmpty(gioHangJson))
            {
                // Nếu giỏ hàng đã tồn tại trong Session, lấy nó từ Session
                chiTietGhs = JsonSerializer.Deserialize<List<ChiTietGh>>(gioHangJson);

                // Tìm món ăn cần xóa dựa trên maMon
                var monCanXoa = chiTietGhs.FirstOrDefault(item => item.MaMon == maMon);

                if (monCanXoa != null)
                {
                    // Xóa món ăn khỏi giỏ hàng
                    chiTietGhs.Remove(monCanXoa);

                    // Lưu lại giỏ hàng sau khi xóa vào Session
                    HttpContext.Session.SetString("GioHang", JsonSerializer.Serialize(chiTietGhs));

                    // Tính lại tổng tiền và gửi nó trong phản hồi JSON
                    var newTotal = chiTietGhs.Sum(item => item.ThanhTien);

                    return Json(new { success = true, newTotal = newTotal });
                }
            }

            // Nếu không thành công, trả về phản hồi JSON với success = false
            return Json(new { success = false });
        }



        [HttpPost]
        public IActionResult CapNhatSoLuong(int maMon, int soLuongMM)
        {
            var gioHangJson = HttpContext.Session.GetString("GioHang");
            List<ChiTietGh> chiTietGhs;

            if (!string.IsNullOrEmpty(gioHangJson))
            {
                chiTietGhs = JsonSerializer.Deserialize<List<ChiTietGh>>(gioHangJson);

                // Tìm món ăn cần cập nhật dựa trên maMon
                var monCanCapNhat = chiTietGhs.FirstOrDefault(item => item.MaMon == maMon);

                if (monCanCapNhat != null)
                {
                    // Cập nhật số lượng món ăn
                    monCanCapNhat.SoLuongMM = soLuongMM;

                    // Cập nhật thành tiền tương ứng
                    monCanCapNhat.ThanhTien = (float)(monCanCapNhat.GiaBan * soLuongMM);

                    // Tính lại tổng tiền sau khi cập nhật thành tiền của sản phẩm
                    var newTotal = chiTietGhs.Sum(item => item.ThanhTien);

                    // Lưu lại giỏ hàng sau khi cập nhật vào Session
                    HttpContext.Session.SetString("GioHang", JsonSerializer.Serialize(chiTietGhs));

                    return Json(new { success = true, newSubtotal = monCanCapNhat.ThanhTien, newTotal = newTotal });
                }
            }

            // Nếu không thành công, trả về phản hồi JSON với success = false
            return Json(new { success = false });
        }

        private int GenerateRandomId()
        {
            Random random = new Random();
            int randomNumber;
            do
            {
                randomNumber = random.Next(1, 1000000); // Tạo số ngẫu nhiên có 4 chữ số
            } while (_context.HanhViNDs.Any(dg => dg.Id == randomNumber)); // Kiểm tra xem số này đã tồn tại trong CSDL chưa
            return randomNumber;
        }

    }
}
