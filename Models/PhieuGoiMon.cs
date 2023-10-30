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

        public string MaPhieuGm { get; set; } = null!;
        public string TenMonAnPgm { get; set; } = null!;
        public DateTime NgayGm { get; set; }
        public string MaBan { get; set; } = null!;
        public string MaHd { get; set; } = null!;
        public string GhiChu { get; set; }

        public virtual BanAn MaBanNavigation { get; set; } = null!;
        public virtual HoaDon MaHdNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietGm> ChiTietGms { get; set; }
    }
}
