using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{ 
    public class _AdminDashboardTotalDestinationCountComponentPartial:ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _AdminDashboardTotalDestinationCountComponentPartial(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _destinationService.GetAllDestinationAsync();
            var totalCount = values.Count;
            return View(totalCount);
        }
    }
}
