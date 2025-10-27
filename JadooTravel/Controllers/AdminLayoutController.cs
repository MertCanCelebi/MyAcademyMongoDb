using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
