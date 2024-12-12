using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class PhieuGiamGium
    {
        public PhieuGiamGium()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaPhieuGg { get; set; } 
        public string MoTa { get; set; } = null!;
        public int PhanTram { get; set; }
        public string LoaiMa { get; set; } = null!;
        public int SoLuongPhieu { get; set; }
        public int Diem { get; set; }
        public int TrangThaiPgg { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
