using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class DatBan
    {
        public string MaLichHen { get; set; } = null!;
        public string TenKh { get; set; } = null!;
        public int SoKhach { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayHen { get; set; }
        public int TrangThaiLh { get; set; }
        public string GhiChu { get; set; } = null!;
        public string MaBan { get; set; } = null!;
        public int MaKH { get; set; } 

        public virtual BanAn MaBanNavigation { get; set; } = null!;
        public virtual KhachHang MaKhNavigation { get; set; } = null!;
    }
}
