using JadooTravel.Services.ReservationServices;
using Microsoft.AspNetCore.Mvc;

namespace JadooTravel.ViewComponents
{
    public class _AdminDashboardTotalReservationCountComponentPartial:ViewComponent
    {
        private readonly IReservationService _reservationService;

        public _AdminDashboardTotalReservationCountComponentPartial(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reservations = await _reservationService.GetAllReservationAsync();
            var totalCount = reservations.Count;
            return View(totalCount);
        }
    }
}
