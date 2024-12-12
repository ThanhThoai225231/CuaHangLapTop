using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;

namespace ThanhThoaiRestaurant.Models
{
	public class SanPhamViewModel
	{
		public MonAn MenuItem { get; set; }
		public IPagedList<DanhGia> DanhGiaList { get; set; }
	}
}
