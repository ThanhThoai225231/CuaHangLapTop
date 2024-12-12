namespace ThanhThoaiRestaurant.Models
{
    public class CPU
    {
        public CPU()
        {
            MonAns = new HashSet<MonAn>();
        }
        public virtual ICollection<MonAn> MonAns { get; set; }
        public int MaCPU { get; set; }
        public string TenLoaiCPU { get; set; }
    }
}
