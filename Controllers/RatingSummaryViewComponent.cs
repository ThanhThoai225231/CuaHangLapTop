using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.ViewComponents
{
    public class RatingSummaryViewComponent : ViewComponent
    {
        private readonly QuanLyNhaHangContext _context;

        public RatingSummaryViewComponent(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int maMon)
        {
            var ratings = _context.DanhGias.Where(dg => dg.MaMon == maMon).ToList();
            var ratingCounts = new int[5]; // Mảng lưu trữ số lượng sao cho mỗi đánh giá từ 1 đến 5
            int totalRatings = ratings.Count;

            foreach (var rating in ratings)
            {
                // Tăng số lượng sao tương ứng với đánh giá
                ratingCounts[rating.Diem - 1]++;
            }

            // Tính số sao trung bình
            double averageRating = 0;
            for (int i = 0; i < 5; i++)
            {
                averageRating += (i + 1) * ratingCounts[i];
            }
            averageRating = totalRatings > 0 ? averageRating / (totalRatings * 5) * 5 : 0;

            ViewBag.AverageRating = averageRating;

            return View(ratingCounts);
        }
    }
}
