namespace ThanhThoaiRestaurant.Models
{
    public class DonHangViewModel
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

        public string MaDonHang { get; set; } = null!;
        public DateTime NgayDatHang { get; set; }
        public int TrangThaiDh { get; set; }
        public decimal PhiVanChuyen { get; set; }
       
        public int MaHd { get; set; }

        public int MaMon { get; set; }
      
        public string TenMonAnDh { get; set; } = null!;
        public int SoLuongMmdh { get; set; }


       
        public DateTime NgayHd { get; set; }

        public float TongTien { get; set; }
        public float TienGiam { get; set; }
        public float TienTt { get; set; }
        public string MaPhieuGg { get; set; } = null!;
        public string HinhThucTT { get; set; } = null!;


        public List<ChiTietDh> ChiTietDonHang { get; set; }
    }
}
