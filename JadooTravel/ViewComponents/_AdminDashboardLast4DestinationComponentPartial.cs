using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _AdminDashboardLast4DestinationComponentPartial:ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _AdminDashboardLast4DestinationComponentPartial(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allDestinations = await _destinationService.GetAllDestinationAsync();

            var last4 = allDestinations
                .OrderByDescending(x => x.DestinationId)
                .Take(4)
                .ToList();

            return View(last4);
        }
    }
}
