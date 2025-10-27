using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _AdminHeaderComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
