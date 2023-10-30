using System;
using System.Collections.Generic;
using X.PagedList;

namespace ThanhThoaiRestaurant.Models
{
    public partial class MonAn
    {
        public MonAn()
        {
            ChiTietDhs = new HashSet<ChiTietDh>();
            ChiTietGms = new HashSet<ChiTietGm>();
            ChiTietHds = new HashSet<ChiTietHd>();
            ChiTietGhs = new HashSet<ChiTietGh>();
        }

        public int MaMon { get; set; }
        public string TenMon { get; set; } 
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }  
        public string MoTaNgan { get; set; } 
        public string MoTaDai { get; set; } 
        public string HinhAnh { get; set; } 
        public int MaNhom { get; set; }
        public string TenNhom { get; set; }

        public int TrangThaiMA { get; set; }

        public virtual NhomMonAn MaNhomNavigation { get; set; }
        public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; }
        public virtual ICollection<ChiTietGm> ChiTietGms { get; set; }
        public virtual ICollection<ChiTietHd> ChiTietHds { get; set; }
        public virtual ICollection<ChiTietGh> ChiTietGhs { get; set; }
         
    }
}
