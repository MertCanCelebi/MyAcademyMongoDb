using JadooTravel.Dtos.DestinationDtos;
using JadooTravel.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JadooTravel.Dtos.ReservationDtos
{
    public class CreateReservationDto
    {
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string DestinationId { get; set; }
    }
}
