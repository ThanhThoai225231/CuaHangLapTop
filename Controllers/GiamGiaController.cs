using Microsoft.AspNetCore.Mvc;
using ThanhThoaiRestaurant.Models;

namespace ThanhThoaiRestaurant.Controllers
{
    [Route("api")]
    public class GiamGiaController : Controller
    {
        private readonly QuanLyNhaHangContext _context;

        public GiamGiaController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

      //  [Route("check-coupon")]
      //  [HttpGet]
      /*  public IActionResult CheckCoupon(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Mã giảm giá không được để trống.");
            }

            var coupon = _context.PhieuGiamGia.FirstOrDefault(c => c.MaPhieuGg == code);

            if (coupon == null)
            {
                return NotFound("Không tìm thấy mã giảm giá.");
            }

            var discountPercent = coupon.PhanTram;



            return Ok(new { valid = true, discountPercent });
        } */
    }
}

