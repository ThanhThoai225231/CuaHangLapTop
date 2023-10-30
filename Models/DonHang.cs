using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDhs = new HashSet<ChiTietDh>();
        }

        public string MaDonHang { get; set; } = null!;
        public DateTime NgayDatHang { get; set; }
        public int TrangThaiDh { get; set; }
        public decimal PhiVanChuyen { get; set; }
        public int MaKH { get; set; } 
        public string MaHd { get; set; } = null!;

        public virtual HoaDon MaHdNavigation { get; set; } = null!;
        public virtual KhachHang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; }
    }
}
