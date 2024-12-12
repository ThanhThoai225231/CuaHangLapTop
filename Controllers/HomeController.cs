using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThanhThoaiRestaurant.Models;
using Microsoft.AspNetCore.Http;

namespace ThanhThoaiRestaurant.Controllers
{
    public class HomeController : Controller
    {

        private readonly QuanLyNhaHangContext _context;
        public HomeController(QuanLyNhaHangContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
      

        public IActionResult Register()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            string tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
            ViewBag.UserName = tenDangNhap;
            return View();
        }
    }
}