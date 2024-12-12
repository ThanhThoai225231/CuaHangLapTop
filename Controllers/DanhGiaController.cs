using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;

namespace ThanhThoaiRestaurant.Controllers
{
	public class DanhGiaController : Controller
	{
		private readonly QuanLyNhaHangContext _context;

		public DanhGiaController(QuanLyNhaHangContext context)
		{
			_context = context;
		}
		public IActionResult Index(int? page, int maMon, int pageSize = 5)
		{
			
			

			var query = _context.DanhGias.Where(d => d.MaMon == maMon);

			


			int pageNumber = page ?? 1;
			var pagedList = query.ToPagedList(pageNumber, pageSize);


			int startItem = (pageNumber - 1) * pageSize + 1;
			int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

			int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
			int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
			int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


			ViewBag.MaMon = maMon;
			ViewBag.TotalItems = pagedList.TotalItemCount;
			ViewBag.TotalPages = pagedList.PageCount;
			ViewBag.PageNumber = pageNumber;
			ViewBag.PageSize = pageSize;
			ViewBag.StartItem = startItem;
			ViewBag.EndItem = endItem;
			ViewBag.MaxVisiblePages = maxVisiblePages;
			ViewBag.StartPage = startPage;
			ViewBag.EndPage = endPage;
			ViewBag.StartPage = startPage;
			ViewBag.EndPage = endPage;
			return View(pagedList);
		}


        public IActionResult Video(int? page, int maMon, int pageSize = 5)
        {



            var query = _context.DanhGias.Where(d => d.MaMon == maMon && (d.HinhAnh1 != null || d.Video != null));




            int pageNumber = page ?? 1;
            var pagedList = query.ToPagedList(pageNumber, pageSize);


            int startItem = (pageNumber - 1) * pageSize + 1;
            int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

            int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
            int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
            int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


            ViewBag.MaMon = maMon;
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.StartItem = startItem;
            ViewBag.EndItem = endItem;
            ViewBag.MaxVisiblePages = maxVisiblePages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            return View(pagedList);
        }

        public IActionResult Rating5(int? page, int maMon, int pageSize = 5)
        {



            var query = _context.DanhGias.Where(d => d.MaMon == maMon && d.Diem == 5);




            int pageNumber = page ?? 1;
            var pagedList = query.ToPagedList(pageNumber, pageSize);


            int startItem = (pageNumber - 1) * pageSize + 1;
            int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

            int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
            int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
            int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


            ViewBag.MaMon = maMon;
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.StartItem = startItem;
            ViewBag.EndItem = endItem;
            ViewBag.MaxVisiblePages = maxVisiblePages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            return View(pagedList);
        }

        public IActionResult Rating4(int? page, int maMon, int pageSize = 5)
        {



            var query = _context.DanhGias.Where(d => d.MaMon == maMon && d.Diem == 4);




            int pageNumber = page ?? 1;
            var pagedList = query.ToPagedList(pageNumber, pageSize);


            int startItem = (pageNumber - 1) * pageSize + 1;
            int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

            int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
            int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
            int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


            ViewBag.MaMon = maMon;
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.StartItem = startItem;
            ViewBag.EndItem = endItem;
            ViewBag.MaxVisiblePages = maxVisiblePages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            return View(pagedList);
        }

        public IActionResult Rating3(int? page, int maMon, int pageSize = 5)
        {



            var query = _context.DanhGias.Where(d => d.MaMon == maMon && d.Diem == 3);




            int pageNumber = page ?? 1;
            var pagedList = query.ToPagedList(pageNumber, pageSize);


            int startItem = (pageNumber - 1) * pageSize + 1;
            int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

            int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
            int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
            int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


            ViewBag.MaMon = maMon;
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.StartItem = startItem;
            ViewBag.EndItem = endItem;
            ViewBag.MaxVisiblePages = maxVisiblePages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            return View(pagedList);
        }

        public IActionResult Rating2(int? page, int maMon, int pageSize = 5)
        {



            var query = _context.DanhGias.Where(d => d.MaMon == maMon && d.Diem == 2);




            int pageNumber = page ?? 1;
            var pagedList = query.ToPagedList(pageNumber, pageSize);


            int startItem = (pageNumber - 1) * pageSize + 1;
            int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

            int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
            int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
            int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


            ViewBag.MaMon = maMon;
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.StartItem = startItem;
            ViewBag.EndItem = endItem;
            ViewBag.MaxVisiblePages = maxVisiblePages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            return View(pagedList);
        }

        public IActionResult Rating1(int? page, int maMon, int pageSize = 5)
        {



            var query = _context.DanhGias.Where(d => d.MaMon == maMon && d.Diem == 1);




            int pageNumber = page ?? 1;
            var pagedList = query.ToPagedList(pageNumber, pageSize);


            int startItem = (pageNumber - 1) * pageSize + 1;
            int endItem = Math.Min(startItem + pageSize - 1, pagedList.TotalItemCount);

            int maxVisiblePages = Math.Min(pagedList.PageCount, 5); // Tối đa 5 trang, nhưng không nhiều hơn tổng số trang
            int startPage = Math.Max(1, pageNumber - (maxVisiblePages / 2));
            int endPage = Math.Min(pagedList.PageCount, startPage + maxVisiblePages - 1);


            ViewBag.MaMon = maMon;
            ViewBag.TotalItems = pagedList.TotalItemCount;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.StartItem = startItem;
            ViewBag.EndItem = endItem;
            ViewBag.MaxVisiblePages = maxVisiblePages;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            return View(pagedList);
        }

    }


}
