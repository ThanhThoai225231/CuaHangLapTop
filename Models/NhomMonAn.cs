using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class NhomMonAn
    {
        public NhomMonAn()
        {
            MonAns = new HashSet<MonAn>();
        }

        public int MaNhom { get; set; } 
        public string TenNhom { get; set; }

        public virtual ICollection<MonAn> MonAns { get; set; }

    }
}
