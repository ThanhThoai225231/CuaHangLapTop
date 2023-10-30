using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThanhThoaiRestaurant.Models
{
    public class GioHang
    {
            [Key]   
            public int MaGioHang { get; set; }
            public int SoLuongMon { get; set; }                 
            public float TongTien { get; set; }

            public virtual ICollection<ChiTietGh> ChiTietGhs { get; set; }

    }
    
    }

