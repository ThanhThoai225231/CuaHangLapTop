using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHds = new HashSet<ChiTietHd>();
            DonHangs = new HashSet<DonHang>();
            PhieuGoiMons = new HashSet<PhieuGoiMon>();
        }

        public string MaHd { get; set; } = null!;
        public DateTime NgayHd { get; set; }
        public string TenMonAnHd { get; set; } = null!;
        public decimal TongTien { get; set; }
        public decimal TienGiam { get; set; }
        public decimal TienTt { get; set; }
        public string MaPhieuGg { get; set; } = null!;

        public virtual PhieuGiamGium MaPhieuGgNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietHd> ChiTietHds { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<PhieuGoiMon> PhieuGoiMons { get; set; }
    }
}
