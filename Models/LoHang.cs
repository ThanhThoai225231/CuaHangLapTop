namespace ThanhThoaiRestaurant.Models
{
    public class LoHang
    {
        public int MaLH { get; set; }
        public DateTime NgayNhap { get; set; }
        public double GiaLo { get; set; }
        public int TrangThaiLH { get; set; }
        public int MaNPP { get; set; }

        // Thiết lập mối quan hệ với ChiTietLH
        public ICollection<ChiTietLH> ChiTietLHs { get; set; } // Một LoHang có nhiều ChiTietLH
    }
}
