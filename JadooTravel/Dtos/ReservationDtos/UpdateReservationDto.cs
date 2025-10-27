namespace JadooTravel.Dtos.ReservationDtos
{
    public class UpdateReservationDto
    {
        public string ReservationId { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string DestinationId { get; set; }
    }
}
