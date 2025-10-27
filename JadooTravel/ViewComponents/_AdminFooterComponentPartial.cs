using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _AdminFooterComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
