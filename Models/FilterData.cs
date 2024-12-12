namespace ThanhThoaiRestaurant.Models
{
    public class FilterData
    {
        public List<string> Category { get; set; }
        public List<string> Hardware { get; set; }
        public List<string> Ram { get; set; }
        public List<string> Cpu { get; set; }
        public List<string> Screen { get; set; }
        public List<string> PriceRange { get; set; }
        public int? PageNumber { get; set; }
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();

    }
}
