namespace ThanhThoaiRestaurant.Models
{
    public class ChiTietLH
    {
        public int MaLH { get; set; }
        public int MaMon { get; set; }
        public int SoLuong { get; set; }
        public int DaBan { get; set; }
        public double GiaNhap { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayNhapLH { get; set; }

        // Thiết lập mối quan hệ với LoHang
        public LoHang LoHang { get; set; } // ChiTietLH thuộc về một LoHang
    }
}
