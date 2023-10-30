using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class ChiTietGm
    {
        public int MaMon { get; set; } 
        public string MaPhieuGm { get; set; } = null!;
        public int SoLuongCt1 { get; set; }

        public virtual MonAn MaMonNavigation { get; set; } = null!;
        public virtual PhieuGoiMon MaPhieuGmNavigation { get; set; } = null!;
    }
}
