using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Default");
        }
    }
}
