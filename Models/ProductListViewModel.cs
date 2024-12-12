namespace ThanhThoaiRestaurant.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<MonAn> MonAns { get; set; } = Enumerable.Empty<MonAn>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
