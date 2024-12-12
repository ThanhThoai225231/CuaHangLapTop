namespace ThanhThoaiRestaurant.Models
{
    public class UserViewModel
    {
        public string TenDN { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string EmailNd { get; set; }

        public string Email { get; set; }
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
    }
}
