using System;
using System.Collections.Generic;


namespace ThanhThoaiRestaurant.Models
{
    public class ThongBao
    {
        public int MaTB { get; set; }
        public string NoiDung { get; set; }
        public int MaHD { get; set; }
        public DateTime ThoiGian { get; set; }
        public int TrangThaiTB { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
