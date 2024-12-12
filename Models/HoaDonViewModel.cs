namespace ThanhThoaiRestaurant.Models
{
    public class HoaDonViewModel
    {
        public int MaKH { get; set; }
        public string TenKh { get; set; } = null!;
        public DateTime NgayTg { get; set; }
        public decimal DoanhSo { get; set; }
        public DateTime NgaySinhKh { get; set; }
        public string GioiTinhKh { get; set; } = null!;
        public string EmailKh { get; set; } = null!;
        public string Sdtkh { get; set; } = null!;
        public string DiaChiKh { get; set; } = null!;
        public int DiemTichLuy { get; set; }
        public string TenDangNhap { get; set; } = null!;

        public int MaHd { get; set; }
        public DateTime NgayHd { get; set; }

        public float TongTien { get; set; }
        public float TienGiam { get; set; }
        public float TienTt { get; set; }
        public int TrangThaiHD { get; set; }
        public string MaPhieuGg { get; set; } = null!;
        public string HinhThucTT { get; set; } = null!;
        public int SoLuongCt { get; set; }
        public float ThanhTien { get; set; }

        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public float GiaBan { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
        public string MoTaNgan { get; set; }
        public string MoTaDai { get; set; }
        public string HinhAnh { get; set; }
        public int MaNhom { get; set; }
        public string TenNhom { get; set; }


        public List<ChiTietHd> ChiTietHoaDon { get; set; }

        public int MaDonHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public int TrangThaiDh { get; set; }
        public decimal PhiVanChuyen { get; set; }
        public string NguoiNhan { get; set; }
        public string SDTNN { get; set; }
        public string DiaChiNhan { get; set; }
        public string GhiChu { get; set; }



        public string TenMonAnDh { get; set; } = null!;
        public int SoLuongMmdh { get; set; }
         public string HinhAnhCt { get; set; }

        public string HinhAnhHd { get; set; }
        public List<ChiTietDh> ChiTietDonHang { get; set; }


        public List<ChiTietGh> ChiTietGioHang { get; set; }
        public int SoLuongMM { get; set; }

        public int MaTB { get; set; }
        public string NoiDung { get; set; }
        public int MaHD { get; set; }

    }
}