using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models;
using System.Linq;
using X.PagedList;
using X.PagedList.Mvc.Core;


namespace ThanhThoaiRestaurant.Controllers
{
    public class ProductController : Controller
    {
        public QuanLyNhaHangContext _context;

        public ProductController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page, int? selectedGroup, string search, string orderby, float? minPrice, float? maxPrice)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            IQueryable<MonAn> query = _context.MonAns;

            // Bộ lọc theo nhóm
            if (selectedGroup.HasValue)
            {
                query = query.Where(m => m.MaNhom == selectedGroup);
            }

            // Bộ lọc theo tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(m => m.TenMon.Contains(search));
            }

            // Bộ lọc theo giá bán
            if (minPrice.HasValue)
            {
                query = query.Where(m => m.GiaBan >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(m => m.GiaBan <= maxPrice);
            }

            var filteredItems = query.ToList(); 

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            int firstItem = (pageNumber - 1) * pageSize + 1;
            int lastItem = Math.Min(pageNumber * pageSize, totalItems);

            
            if (orderby == "price-asc")
            {
                filteredItems = filteredItems.OrderBy(m => m.GiaBan).ToList(); 
            }
            else if (orderby == "price-desc")
            {
                filteredItems = filteredItems.OrderByDescending(m => m.GiaBan).ToList();
            }

            var pagedList = filteredItems.ToPagedList(pageNumber, pageSize); 

            ViewBag.SelectedGroup = selectedGroup;
            ViewBag.SearchTerm = search;
            ViewBag.OrderBy = orderby;

            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.PagingInfo = $"Sản phẩm {firstItem} đến {lastItem} trên tổng số {totalItems} sản phẩm";

            return View(pagedList);
        }





        public IActionResult Details(int id)
		{
			var menuItem = _context.MonAns.FirstOrDefault(m => m.MaMon == id);

			if (menuItem == null)
			{
				return NotFound();
			}

			ViewBag.MenuItem = menuItem; // Truyền dữ liệu món ăn vào ViewBag
			return View(menuItem);
		}



	}
}
