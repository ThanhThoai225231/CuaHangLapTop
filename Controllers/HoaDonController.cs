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
using ThanhThoaiRestaurant.Services;

namespace ThanhThoaiRestaurant.Controllers
{
    public class HoaDonController : Controller
    {

        private readonly QuanLyNhaHangContext _context;

        private readonly IVnPayService _vnPayService;

        private readonly IMailService _mailService;
        //injecting IMailService vào constructor
        private readonly IPayPalService _payPalService;
        private readonly IShippingService _shippingService;

       
        public HoaDonController(QuanLyNhaHangContext context, IVnPayService vnPayService, IMailService _MailService, IPayPalService payPalService, IShippingService shippingService)
        {
            _context = context;
            _vnPayService = vnPayService;
            _mailService = _MailService;
            _payPalService = payPalService;
            _shippingService = shippingService;

        }
        public IActionResult Index(int maKM)
        {
            ViewBag.MaKM = maKM;
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

            var khuyenMai = _context.KhuyenMais.SingleOrDefault(km => km.MaKM == maKM && km.MaKH == khachHang.MaKH);

            if (khuyenMai == null && maKM != 0)
            {
                ViewBag.ErrorMessage = "Mã giảm giá không hợp lệ.";
                // Hoặc có thể sử dụng TempData nếu muốn giữ thông báo qua một redirect
                // TempData["ErrorMessage"] = "Mã giảm giá không hợp lệ.";
            }


            var gioHang = HttpContext.Session.GetObject<List<ChiTietGh>>("GioHang");

            

            // Tạo một danh sách tạm thời để lưu thông tin món ăn
            var danhSachMonAn = new List<ChiTietHd>();

            float TongTien = 0;
            float TongGiamGia = 0;
            if (khuyenMai != null)
            {
                 TongGiamGia = (float)khuyenMai.GiaGiam;
                HttpContext.Session.SetInt32("MaKM", khuyenMai.MaKM);

            }
            
            HttpContext.Session.SetString("TongGiamGia", TongGiamGia.ToString());

            foreach (var item in gioHang)
            {
                ChiTietHd chiTietHd = new ChiTietHd
                {
                    TenMon = item.TenMon,
                    SoLuongCt = item.SoLuongMM,
                    ThanhTien = item.ThanhTien
                };

                danhSachMonAn.Add(chiTietHd);
                TongTien += (float)chiTietHd.ThanhTien;
            }

            

            float TienThanhToan = TongTien - TongGiamGia;
            // Lặp qua danh sách sản phẩm trong giỏ hàng và thêm thông tin món ăn vào danh sách tạm thời
            

            // Tạo một đối tượng HoaDonViewModel và điền thông tin khách hàng vào
            var hoaDonViewModel = new HoaDonViewModel
            {

                TenKh = khachHang.TenKh,
                EmailKh = khachHang.EmailKh,
                Sdtkh = khachHang.Sdtkh,
                DiaChiKh = khachHang.DiaChiKh,
                GioiTinhKh = khachHang.GioiTinhKh,

                ChiTietHoaDon = danhSachMonAn,

                TongTien = TongTien,
                TienGiam = TongGiamGia,
                TienTt = TienThanhToan

                // Điền các thuộc tính khác của khách hàng
            };    
            return View(hoaDonViewModel);
        }

        public IActionResult LoadVouchers ()
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

            var khuyenMai = _context.KhuyenMais.Where(km =>  km.MaKH == khachHang.MaKH).ToList();
            return View(khuyenMai);
        }

        [HttpPost]
        public IActionResult XacNhanThanhToan(HoaDonViewModel model)
        {
            float TongGiamGia = float.Parse(HttpContext.Session.GetString("TongGiamGia"));
            var maKM = HttpContext.Session.GetInt32("MaKM");

            var khuyenMai1 = _context.KhuyenMais.SingleOrDefault(km => km.MaKM == maKM);
            if (khuyenMai1 != null) {
                _context.KhuyenMais.Remove(khuyenMai1);
                _context.SaveChanges();
                    }

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
            // Lấy thông tin giỏ hàng từ Session
            var gioHang = HttpContext.Session.GetObject<List<ChiTietGh>>("GioHang");

            if (gioHang == null || gioHang.Count == 0)
            {
                // Xử lý trường hợp giỏ hàng rỗng
                // Có thể chuyển người dùng đến trang thông báo lỗi hoặc trang giỏ hàng
                return View("GioHangRong");
            }

            var danhSachMonAn = new List<ChiTietHd>();

            float TongTien = 0;
            float TienGiam = TongGiamGia;

            // Lặp qua danh sách món ăn trong giỏ hàng để tạo danh sách chi tiết hoá đơn
            foreach (var item in gioHang)
            {
                ChiTietHd chiTietHd = new ChiTietHd
                {
                    MaMon = item.MaMon,
                    TenMon = item.TenMon,
                    SoLuongCt = item.SoLuongMM,
                    ThanhTien = item.ThanhTien,
                    HinhAnhHd = item.HinhAnh
                };

                danhSachMonAn.Add(chiTietHd);
                TongTien += (float)chiTietHd.ThanhTien;
            }
              
            float TienTt = (TongTien - TienGiam);

            // Lưu hoá đơn vào CSDL
            var hoaDon = new HoaDon
            {
                MaHd = GenerateRandomCustomerCode(),
                NgayHd = DateTime.Now,
                TongTien = TongTien,
                TienGiam = TienGiam,
                TienTt = TienTt,
                MaPhieuGg = 001,
                HinhThucTT = model.HinhThucTT,
                TrangThaiHD = 1
               
            };

            // Lưu hoá đơn vào CSDL
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();



            // Lấy mã hoá đơn sau khi đã lưu vào CSDL
            int maHoaDon = hoaDon.MaHd;


			if (TongTien > 20000000 && TongTien < 30000000)
			{
				var khuyenMai = new KhuyenMai
				{
					MaKM = hoaDon.MaHd,
					TenKM = "Giảm giá 100.000 VNĐ cho đơn hàng",
					GiaGiam = 100000,
					NgayBD = DateTime.Now,
					NgayKT = DateTime.Now.AddDays(30),
					MaKH = khachHang.MaKH


				};
				_context.KhuyenMais.Add(khuyenMai);
				_context.SaveChanges();

				var mailData1 = new MailData
				{
					ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
					ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
					Title = "Thông báo khuyến mãi",
					                    Body = "Chào " + khachHang.TenKh + ",\n\n"
                    + "Chúng tôi rất vui thông báo đến bạn về mã khuyến mãi đặc biệt:\n\n"
                    + "Mã khuyến mãi: " + khuyenMai.MaKM + "\n"
                    + "Tên khuyến mãi: " + khuyenMai.TenKM + "\n"
                    + "Giá giảm: " + khuyenMai.GiaGiam.ToString("N0") + "" + "VNĐ" + "\n"
                    + "Thời gian bắt đầu: " + khuyenMai.NgayBD.ToString("dd/MM/yyyy") + "\n"
                    + "Thời gian kết thúc: " + khuyenMai.NgayKT.ToString("dd/MM/yyyy") + "\n\n"
                    + "Hãy sử dụng mã này để nhận ưu đãi đặc biệt khi mua hàng tiếp theo tại cửa hàng của chúng tôi.\n\n"
                    + "Chúc bạn mua sắm vui vẻ và tiết kiệm!"
				};
				// Gửi email
				_mailService.SendMail(mailData1);
			}


			if (TongTien > 30000000 && TongTien < 50000000)
			{
				var khuyenMai = new KhuyenMai
				{
					MaKM = hoaDon.MaHd,
					TenKM = "Giảm giá 200.000 VNĐ cho đơn hàng",
					GiaGiam = 200000,
					NgayBD = DateTime.Now,
					NgayKT = DateTime.Now.AddDays(30),
					MaKH = khachHang.MaKH
				};
				_context.KhuyenMais.Add(khuyenMai);
				_context.SaveChanges();

				var mailData1 = new MailData
				{
					ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
					ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
					Title = "Thông báo khuyến mãi",
					Body = "Chào " + khachHang.TenKh + ",\n\n"
                    + "Chúng tôi rất vui thông báo đến bạn về mã khuyến mãi đặc biệt:\n\n"
                    + "Mã khuyến mãi: " + khuyenMai.MaKM + "\n"
                    + "Tên khuyến mãi: " + khuyenMai.TenKM + "\n"
                    + "Giá giảm: " + khuyenMai.GiaGiam.ToString("N0") + "" + "VNĐ" + "\n"
                    + "Thời gian bắt đầu: " + khuyenMai.NgayBD.ToString("dd/MM/yyyy") + "\n"
                    + "Thời gian kết thúc: " + khuyenMai.NgayKT.ToString("dd/MM/yyyy") + "\n\n"
                    + "Hãy sử dụng mã này để nhận ưu đãi đặc biệt khi mua hàng tiếp theo tại cửa hàng của chúng tôi.\n\n"
                    + "Chúc bạn mua sắm vui vẻ và tiết kiệm!"
				};
				// Gửi email
				_mailService.SendMail(mailData1);
			}

			if (TongTien >= 50000000)
			{
				var khuyenMai = new KhuyenMai
				{
					MaKM = hoaDon.MaHd,
					TenKM = "Giảm giá 500.000 VNĐ cho đơn hàng",
					GiaGiam = 500000,
					NgayBD = DateTime.Now,
					NgayKT = DateTime.Now.AddDays(30),
					MaKH = khachHang.MaKH
				};
				_context.KhuyenMais.Add(khuyenMai);
				_context.SaveChanges();

				var mailData1 = new MailData
				{
					ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
					ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
					Title = "Thông báo khuyến mãi",
					Body = "Chào " + khachHang.TenKh + ",\n\n"
                    + "Chúng tôi rất vui thông báo đến bạn về mã khuyến mãi đặc biệt:\n\n"
                    + "Mã khuyến mãi: " + khuyenMai.MaKM + "\n"
                    + "Tên khuyến mãi: " + khuyenMai.TenKM + "\n"
                    + "Giá giảm: " + khuyenMai.GiaGiam.ToString("N0") + "" + "VNĐ" + "\n"
                    + "Thời gian bắt đầu: " + khuyenMai.NgayBD.ToString("dd/MM/yyyy") + "\n"
                    + "Thời gian kết thúc: " + khuyenMai.NgayKT.ToString("dd/MM/yyyy") + "\n\n"
                    + "Hãy sử dụng mã này để nhận ưu đãi đặc biệt khi mua hàng tiếp theo tại cửa hàng của chúng tôi.\n\n"
                    + "Chúc bạn mua sắm vui vẻ và tiết kiệm!"
				};
				// Gửi email
				_mailService.SendMail(mailData1);
			}

			// Lưu chi tiết hoá đơn vào CSDL
			foreach (var chiTiet in danhSachMonAn)
            {
                var chiTietHoaDon = new ChiTietHd
                {
                    MaHd = maHoaDon,
                    MaMon = chiTiet.MaMon,
                    TenMon = chiTiet.TenMon,
                    SoLuongCt = chiTiet.SoLuongCt,
                    ThanhTien = chiTiet.ThanhTien,
                    HinhAnhHd = chiTiet.HinhAnhHd

                    // Điền các thông tin chi tiết hoá đơn từ Model khác
                };
                _context.ChiTietHds.Add(chiTietHoaDon);
                var monAn = _context.MonAns.SingleOrDefault(ma => ma.MaMon == chiTiet.MaMon);
                if (monAn != null)
                {
                    monAn.SoLuong -= chiTiet.SoLuongCt;
                    monAn.SoLuongDaBan += chiTiet.SoLuongCt;
                }

                var chiTietLoHangs = _context.ChiTietLHs
                    .Where(ct => ct.MaMon == monAn.MaMon)
                    .OrderBy(ct => ct.NgayNhapLH)
                    .ToList();

                if (chiTietLoHangs != null)
                {

                    int remainingQuantity = chiTiet.SoLuongCt;

                    foreach (var loHang in chiTietLoHangs)
                    {
                        int availableQuantity = (int)(loHang.SoLuong - loHang.DaBan);

                        if (availableQuantity >= remainingQuantity)
                        {
                            loHang.DaBan += remainingQuantity;
                            _context.Update(loHang);
                            remainingQuantity = 0;
                            break;
                        }
                        else
                        {
                            loHang.DaBan = loHang.SoLuong; // bán hết lô hàng này
                            _context.Update(loHang);
                            remainingQuantity -= availableQuantity;
                        }
                        _context.SaveChanges();
                    }
                    _context.SaveChanges();
                }
            }

            _context.SaveChanges();

            // Xóa dữ liệu giỏ hàng của người dùng sau khi đã lưu vào hoá đơn
            HttpContext.Session.Remove("GioHang");


            var donHang = new DonHang
            {
                MaDonHang = maHoaDon,
                NgayDatHang = DateTime.Now,
                TrangThaiDh = 1,
                MaKH = khachHang.MaKH,
                MaHd = maHoaDon,
                NguoiNhan = model.NguoiNhan,
                SDTNN = model.SDTNN,
                DiaChiNhan = model.DiaChiNhan,
                GhiChu = model.GhiChu

            };

            _context.DonHangs.Add(donHang);
            _context.SaveChanges();


            foreach (var chiTiet in danhSachMonAn)
            {
                var chiTietDonHang = new ChiTietDh
                {
                    MaDonHang = maHoaDon, // Gán mã đơn hàng
                    MaMon = chiTiet.MaMon,
                    SoLuongMmdh = chiTiet.SoLuongCt,
                    TenMonAnDh = chiTiet.TenMon,
                    HinhAnhCt = chiTiet.HinhAnhHd
                };
                _context.ChiTietDhs.Add(chiTietDonHang);

                if (!string.IsNullOrEmpty(tenDangNhap))
                {
                    var hanhViND = new HanhViND
                    {
                        Id = GenerateRandomId(),
                        TenDangNhap = tenDangNhap,
                        MaMon = chiTiet.MaMon,
                        ThoiGian = DateTime.Now,
                        HanhDong = 3 
                    };

                    _context.HanhViNDs.Add(hanhViND);
                    _context.SaveChanges();
                }
            }
            _context.SaveChanges(); 

            var thongbao = new ThongBao
            {
                MaTB = maHoaDon,
                NoiDung = $"Vừa có đơn hàng mới chờ xét duyệt - Mã đơn hàng: {maHoaDon}",
                MaHD = maHoaDon,
                ThoiGian = DateTime.Now,
                TrangThaiTB = 1

            };
            _context.ThongBaos.Add(thongbao);
            _context.SaveChanges();


			if (model.HinhThucTT == "Chuyển khoản")
            {
                var paymentModel = new PaymentInformationModel
                {
                    OrderType = "Laptop",
                    Amount = TienTt, // Số tiền cần thanh toán

                    OrderDescription = "Thanh toán đơn hàng" + hoaDon.MaHd, // Mô tả đơn hàng
                    Name = khachHang.TenKh // Tên khách hàng (hoặc thông tin khác cần thiết)
                };

                // Gọi IVnPayService để tạo URL thanh toán VNPay
                var paymentUrl = _vnPayService.CreatePaymentUrl(paymentModel, HttpContext);

                var mailData1 = new MailData
                {
                    ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
                    ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
                    Title = "Thông báo đặt hàng thành công",
                    Body = "Chào " + khachHang.TenKh + ",\n\n"
            + "Đơn hàng của bạn đã được xác nhận thành công. Dưới đây là chi tiết đơn hàng:\n\n"
            + "Mã đơn hàng: " + hoaDon.MaHd + "\n"
            + "Ngày đặt hàng: " + hoaDon.NgayHd.ToString("dd/MM/yyyy") + "\n"
            + "Danh sách sản phẩm:\n"
                };

                foreach (var chiTiet in danhSachMonAn)
                {
                    mailData1.Body += "Tên sản phẩm: " + chiTiet.TenMon + "\n"
                                     + "Số lượng: " + chiTiet.SoLuongCt + "\n"
                                     + "Thành tiền: " + chiTiet.ThanhTien.ToString("N0") + "" + "VNĐ" + "\n";

                }

                mailData1.Body += "Tổng tiền: " + hoaDon.TongTien.ToString("N0") + "" + "VNĐ" + "\n"
                                 + "Tổng tiền giảm: " + hoaDon.TienGiam.ToString("N0") + "" + "VNĐ" + "\n"
                                 + "Tổng tiền thanh toán: " + hoaDon.TienTt.ToString("N0") + "" + "VNĐ" + "\n\n"
                                 + "Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi.";

                // Gửi email
                _mailService.SendMail(mailData1);
                // Chuyển hướng sang trang thanh toán của VNPay
                return Redirect(paymentUrl);
            }


            if (model.HinhThucTT == "Chuyển khoản PayPal")
            {
                var paymentModel = new PaymentInformationModel1
                {
                    OrderType = "Laptop",
                    Amount = TienTt, // Số tiền cần thanh toán

                    OrderDescription = "Thanh toán đơn hàng" + hoaDon.MaHd, // Mô tả đơn hàng
                    Name = khachHang.TenKh // Tên khách hàng (hoặc thông tin khác cần thiết)
                };

                // Gọi IVnPayService để tạo URL thanh toán VNPay
                var paymentUrl = _payPalService.CreatePaymentUrl(paymentModel, HttpContext);
                return Redirect(paymentUrl);
            }

                var mailData = new MailData
            {
                ReceiverName = khachHang.TenKh, // Thay "Tên khách hàng" bằng tên thực sự của khách hàng
                ReceiverEmail = khachHang.EmailKh, // Thay "email@example.com" bằng địa chỉ email thực sự của khách hàng
                Title = "Thông báo đặt hàng thành công",
                Body = "Chào " + khachHang.TenKh + ",\n\n"
            + "Đơn hàng của bạn đã được xác nhận thành công. Dưới đây là chi tiết đơn hàng:\n\n"
            + "Mã đơn hàng: " + hoaDon.MaHd + "\n"
            + "Ngày đặt hàng: " + hoaDon.NgayHd.ToString("dd/MM/yyyy") + "\n"
            + "Danh sách sản phẩm:\n"
            };

            foreach (var chiTiet in danhSachMonAn)
            {
                mailData.Body += "Tên sản phẩm: " + chiTiet.TenMon + "\n"
                                 + "Số lượng: " + chiTiet.SoLuongCt + "\n"
                                 + "Thành tiền: " + chiTiet.ThanhTien.ToString("N0")+  "" + "VNĐ" + "\n";
                                 
            }

            mailData.Body += "Tổng tiền: " + hoaDon.TongTien.ToString("N0") + ""  +"VNĐ" + "\n"
                             + "Tổng tiền giảm: " + hoaDon.TienGiam.ToString("N0") + ""  + "VNĐ" + "\n"
                             + "Tổng tiền thanh toán: " + hoaDon.TienTt.ToString("N0") + "" +  "VNĐ" + "\n\n"
                             + "Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi.";

            // Gửi email
            _mailService.SendMail(mailData);
            bool isOrderShipped = _shippingService.ShipOrder(model).Result;


            
            


            // Chuyển người dùng đến trang cảm ơn hoặc trang xác nhận hoá đơn
            return Redirect("/GioHang");


        }

        public int GenerateRandomCustomerCode()
        {
            Random random = new Random();
            int customerCode;

            do
            {
                customerCode = random.Next(1000, 9999); // Sinh số ngẫu nhiên có 4 chữ số
            } while (_context.HoaDons.Any(kh => kh.MaHd == customerCode)); // Kiểm tra xem mã đã tồn tại trong CSDL hay chưa

            return customerCode;
        }

		public int GenerateRandomMaKM()
		{
			Random random = new Random();
			int customerCode;

			do
			{
				customerCode = random.Next(1000, 9999); // Sinh số ngẫu nhiên có 4 chữ số
			} while (_context.KhuyenMais.Any(kh => kh.MaKM == customerCode)); // Kiểm tra xem mã đã tồn tại trong CSDL hay chưa

			return customerCode;
		}

		[HttpPost]
        public IActionResult SendMail(MailData mailData)
        {
            _mailService.SendMail(mailData);
            return View();
        }

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
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
