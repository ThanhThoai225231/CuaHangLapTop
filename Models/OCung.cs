namespace ThanhThoaiRestaurant.Models
{
    public class OCung
    {
        public OCung()
        {
            MonAns = new HashSet<MonAn>();
        }
        public virtual ICollection<MonAn> MonAns { get; set; }
        public int MaOC { get; set; }
        public string DungLuong { get; set; }
    }
}
