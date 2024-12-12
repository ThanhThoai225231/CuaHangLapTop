using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThanhThoaiRestaurant.Models
{
    public partial class ChiTietDh
    {
        public int MaMon { get; set; } 
        public int MaDonHang { get; set; } 
        public string TenMonAnDh { get; set; } = null!;
        public int SoLuongMmdh { get; set; }
        
        public string HinhAnhCt { get; set; }

        

        public virtual DonHang MaDonHangNavigation { get; set; } 
        public virtual MonAn MaMonNavigation { get; set; } 
    }
}
