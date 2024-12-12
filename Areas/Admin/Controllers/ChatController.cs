using Microsoft.AspNetCore.Mvc;

namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChatController : Controller
    {

        public IActionResult Index()
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
            ViewBag.UserName = tenDangNhap;
            return View();
        }
    }
}
