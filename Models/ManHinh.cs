namespace ThanhThoaiRestaurant.Models
{
    public class ManHinh
    {
        public ManHinh()
        {
            MonAns = new HashSet<MonAn>();
        }
        public virtual ICollection<MonAn> MonAns { get; set; }
        public int MaMH { get; set; }
        public string KichThuoc { get; set; }
    }
}
