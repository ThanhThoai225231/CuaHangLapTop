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
            public float TongGiamGia { get; set; }
            public float TienThanhToan { get; set; }

        public virtual ICollection<ChiTietGh> ChiTietGhs { get; set; }
        public void CapNhatTongTien()
        {
            if (ChiTietGhs != null && ChiTietGhs.Any())
            {
                TongTien = ChiTietGhs.Sum(ct => ct.ThanhTien);
            }
            else
            {
                TongTien = 0;
            }
        }

        public void CapNhatTongGiamGia()
        {
            // Logic tính tổng giảm giá dựa trên mã giảm giá hoặc các quy tắc của bạn
            // Ví dụ đơn giản:
            TongGiamGia = 0;
        }

        public void CapNhatTienThanhToan()
        {
            TienThanhToan = TongTien - TongGiamGia;
        }
    }
}
    
    

