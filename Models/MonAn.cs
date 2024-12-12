using System;
using System.Collections.Generic;
using X.PagedList;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Key]
        public int MaMon { get; set; }
        public string TenMon { get; set; } 
        public double GiaBan { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongDaBan { get; set; }
        public string DonViTinh { get; set; }  
        public string MoTaNgan { get; set; } 
        public string MoTaDai { get; set; } 
        public string HinhAnh { get; set; }
		public string HinhAnh1 { get; set; }
		public string HinhAnh2 { get; set; }
		public string HinhAnh3 { get; set; }
        [ForeignKey("NhomMonAn")]		
		public int MaNhom { get; set; }
        public string TenNhom { get; set; }
        
        public double GiaGoc { get; set; }
        public double GiaKhuyenMai { get; set; }

        public int TrangThaiMA { get; set; }
        public string GhiChu { get; set; } = null!;
        public DateTime NgayMoBan { get; set; }

        [ForeignKey("OCung")]
        public int MaOC { get; set; }
        public OCung OCung { get; set; }

        [ForeignKey("RAM")]
        public int MaRam { get; set; }

        public RAM RAM { get; set; }

        [ForeignKey("ManHinh")]
        public int MaMH { get; set; }
        public ManHinh ManHinh { get; set; }

        [ForeignKey("CPU")]
        public int MaCPU { get; set; }
        public CPU CPU { get; set; }
        public virtual NhomMonAn MaNhomNavigation { get; set; }
       
        
        public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; }
        public virtual ICollection<ChiTietGm> ChiTietGms { get; set; }
        public virtual ICollection<ChiTietHd> ChiTietHds { get; set; }
        public virtual ICollection<ChiTietGh> ChiTietGhs { get; set; }
         
    }
}
