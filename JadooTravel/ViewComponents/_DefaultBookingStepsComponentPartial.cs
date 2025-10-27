using JadooTravel.Dtos.ReservationDtos;
using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JadooTravel.ViewComponents
{
    public class _DefaultBookingStepsComponentPartial:ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _DefaultBookingStepsComponentPartial(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var destinations = await _destinationService.GetAllDestinationAsync();
            ViewBag.Destinations = new SelectList(destinations, "DestinationId", "CityCountry");

            return View(new CreateReservationDto());
        }
    }
}
