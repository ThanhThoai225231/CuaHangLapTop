using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class BanAn
    {
        public BanAn()
        {
            PhieuGoiMons = new HashSet<PhieuGoiMon>();
        }

        public string MaBan { get; set; } = null!;
        public int SucChua { get; set; }
        public int TrangThaiBa { get; set; }

        public virtual ICollection<PhieuGoiMon> PhieuGoiMons { get; set; }
    }
}
