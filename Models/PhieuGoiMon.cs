using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class PhieuGoiMon
    {
        public PhieuGoiMon()
        {
            ChiTietGms = new HashSet<ChiTietGm>();
        }

        public int MaPhieuGm { get; set; } 
        public string TenMonAnPgm { get; set; } = null!;
        public DateTime NgayGm { get; set; }
        public string MaBan { get; set; } = null!;
        public int MaHd { get; set; } 
        public string GhiChu { get; set; }

        public virtual BanAn MaBanNavigation { get; set; } = null!;
        public virtual HoaDon MaHdNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietGm> ChiTietGms { get; set; }
    }
}
