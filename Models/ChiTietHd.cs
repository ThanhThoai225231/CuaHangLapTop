using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class ChiTietHd
    {
        public int MaMon { get; set; } 
        public int MaHd { get; set; } 
        public int SoLuongCt { get; set; }
        public double ThanhTien { get; set; }
        public string TenMon { get; set; } = null!;

       public string HinhAnhHd { get; set; }
        public virtual HoaDon MaHdNavigation { get; set; } 
        public virtual MonAn MaMonNavigation { get; set; } 
    }
}
