using Microsoft.AspNetCore.Mvc;

namespace ThanhThoaiRestaurant.Controllers
{
    public class GioiThieuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
