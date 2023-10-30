using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class ChiTietHd
    {
        public int MaMon { get; set; } 
        public string MaHd { get; set; } = null!;
        public int SoLuongCt { get; set; }
        public decimal? ThanhTien { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; } = null!;
        public virtual MonAn MaMonNavigation { get; set; } = null!;
    }
}
