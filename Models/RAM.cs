namespace ThanhThoaiRestaurant.Models
{
    public class RAM
    {
        public RAM()
        {
            MonAns = new HashSet<MonAn>();
        }
        public virtual ICollection<MonAn> MonAns { get; set; }
        public int MaRam { get; set; }
        public string DungLuongRam { get; set; }
    }
}
