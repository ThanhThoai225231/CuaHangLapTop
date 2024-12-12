using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class NhanVien
    {
        public string MaNv { get; set; } = null!;
        public string TenNv { get; set; } = null!;
        public DateTime NgayVl { get; set; }
        public string Sdtnv { get; set; } = null!;
        public string EmailNv { get; set; } = null!;
        public string GioiTinhNv { get; set; } = null!;
        public string DiaChiNv { get; set; } = null!;
        public string ChucVu { get; set; } = null!;
        public DateTime NgaySinhNv { get; set; }
        public string Cccdnv { get; set; } = null!;
        public string TenDangNhap { get; set; } = null!;
        public string HinhAnh { get; set; } = null!;

        public virtual NguoiDung TenDangNhapNavigation { get; set; } = null!;
    }
}
