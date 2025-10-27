using JadooTravel.Services.DestinationServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JadooTravel.ViewComponents
{
    public class _AdminDashboardCapasityOfDestinationComponentPartial:ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public _AdminDashboardCapasityOfDestinationComponentPartial(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _destinationService.GetAllDestinationAsync();
            foreach (var v in values)
            {
                Console.WriteLine($"{v.CityCountry} - {v.Capacity}");
            }
            return View(values);
        }
    }
}
