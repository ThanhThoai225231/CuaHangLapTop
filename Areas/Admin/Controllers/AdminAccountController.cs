using Microsoft.AspNetCore.Mvc;

namespace ThanhThoaiRestaurant.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        [HttpGet]
        public IActionResult AdminLogout()
        {
            // Xóa tất cả các session liên quan đến đăng nhập
            HttpContext.Session.Clear();

            // Sau đó, chuyển hướng đến trang đăng ký hoặc bất kỳ trang nào bạn muốn
            return RedirectToAction("Login", "Account");
        }

        public IActionResult DenyAccess()
        {
            return View("DenyAccess");
        }
    }
}
