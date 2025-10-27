using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _AdminDashboardTotalCapacityOfDestinationsComponentPartial:ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _AdminDashboardTotalCapacityOfDestinationsComponentPartial(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await _destinationService.GetAllDestinationAsync();
            var totalCapacity = destinations.Sum(x => x.Capacity);
            return View(totalCapacity);
        }
    }
}
