using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
            GioHangs = new HashSet<GioHang>();
        }

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

        public virtual NguoiDung TenDangNhapNavigation { get; set; } = null!;
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
