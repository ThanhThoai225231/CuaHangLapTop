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

        public int MaHd { get; set; } 
        public DateTime NgayHd { get; set; }
       
        public double TongTien { get; set; }
        public double TienGiam { get; set; }
        public double TienTt { get; set; }
        public int MaPhieuGg { get; set; } 
        public string HinhThucTT { get; set; } = null!;
        public int TrangThaiHD { get; set; }
        public virtual PhieuGiamGium MaPhieuGgNavigation { get; set; } 
        public virtual ICollection<ChiTietHd> ChiTietHds { get; set; }
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual ICollection<PhieuGoiMon> PhieuGoiMons { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
