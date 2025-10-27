using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Dtos.ReservationDtos;

namespace JadooTravel.Models
{
    public class CreateReservationPageViewModel
    {
        public CreateReservationDto Reservation { get; set; }
        public List<ResultDestinationDto> DestinationList { get; set; }
    }
}
