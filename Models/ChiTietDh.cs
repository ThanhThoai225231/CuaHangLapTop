using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class ChiTietDh
    {
        public int MaMon { get; set; } 
        public string MaDonHang { get; set; } = null!;
        public string TenMonAnDh { get; set; } = null!;
        public int SoLuongMmdh { get; set; }

        public virtual DonHang MaDonHangNavigation { get; set; } = null!;
        public virtual MonAn MaMonNavigation { get; set; } = null!;
    }
}
