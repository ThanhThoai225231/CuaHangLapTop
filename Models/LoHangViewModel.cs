namespace ThanhThoaiRestaurant.Models
{
    public class LoHangViewModel
    {
        public int MaLH { get; set; }
        public DateTime NgayNhap { get; set; }
        public double GiaLo { get; set; }
        public int TrangThaiLH { get; set; }
        public int MaNPP { get; set; }      
        public int MaMon { get; set; }
        public int SoLuong { get; set; }
        public int DaBan { get; set; }
        public double GiaNhap { get; set; }
        public string HinhAnh { get; set; }
        public string TenSanPham { get; set; }
        public DateTime NgayNhapLH { get; set; }
        public List<ChiTietLH> ChiTietLoHangs { get; set; }
    }
}
