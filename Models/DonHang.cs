using System;
using System.Collections.Generic;

namespace ThanhThoaiRestaurant.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDhs = new HashSet<ChiTietDh>();
        }

        public int MaDonHang { get; set; }
        public DateTime NgayDatHang { get; set; }
        public int TrangThaiDh { get; set; }
        public decimal PhiVanChuyen { get; set; }
        public int MaKH { get; set; }
        public int MaHd { get; set; }
        public string NguoiNhan { get; set; }
        public string SDTNN { get; set; }
        public string DiaChiNhan { get; set; }
        public string GhiChu { get; set; }

       
        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual KhachHang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<ChiTietDh> ChiTietDhs { get; set; }
    }
}
