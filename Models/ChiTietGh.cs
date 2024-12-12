using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public class ChiTietGh
    {
        public int MaMon { get; set; }
        public int MaGioHang { get; set; } 
        public int SoLuongMM { get; set; }

        public int SoLuong { get; set; }
        public string TenMon { get; set; }

        public string HinhAnh { get; set; }

        public float GiaBan { get; set; }

		public float GiaKhuyenMai { get; set; }

		public float ThanhTien { get; set; }
        public virtual GioHang MaGioHangNavigation { get; set; } = null!;
        public virtual MonAn MaMonNavigation { get; set; } = null!;


    }
}
