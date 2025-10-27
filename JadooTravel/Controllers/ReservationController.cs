using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Dtos.ReservationDtos;
using JadooTravel.Entities;
using JadooTravel.Models;
using JadooTravel.Services.DestinationServices;
using JadooTravel.Services.ReservationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JadooTravel.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IDestinationService _destinationService;

        public ReservationController(IReservationService reservationService, IDestinationService destinationService)
        {
            _reservationService = reservationService;
            _destinationService = destinationService;
        }

        public async Task<IActionResult> ReservationList()
        {
            var reservations = await _reservationService.GetAllReservationAsync();
            return View(reservations); 
        }

        public async Task<IActionResult> CreateReservation()
        {
            var destinations = await _destinationService.GetAllDestinationAsync();
            ViewBag.Destinations = new SelectList(destinations, "DestinationId", "CityCountry");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDto dto)
        {
            await _reservationService.CreateReservationAsync(dto);
            TempData["SuccessMessage"] = "Rezervasyonunuz başarıyla eklenmiştir!";
            return RedirectToAction("Index","Default");
        }

        public async Task<IActionResult> UpdateReservation(string id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
                return NotFound();

            var updateDto = new UpdateReservationDto
            {
                ReservationId = reservation.ReservationId,
                NameSurname = reservation.NameSurname,
                PhoneNumber = reservation.PhoneNumber,
                Email = reservation.Email,
                Note = reservation.Note,
                DestinationId = reservation.DestinationId
            };

            var destinations = await _destinationService.GetAllDestinationAsync();

            ViewBag.Destinations = new SelectList(destinations, "DestinationId", "CityCountry", reservation.DestinationId);
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservation(UpdateReservationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _reservationService.UpdateReservationAsync(dto);
            return RedirectToAction("ReservationList");
        }

        public async Task<IActionResult> DeleteReservation(string id)
        {
            await _reservationService.DeleteReservationAsync(id);
            return RedirectToAction("ReservationList");
        }
    }
}
