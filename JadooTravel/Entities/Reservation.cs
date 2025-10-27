using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JadooTravel.Entities
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReservationId { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string DestinationId { get; set; }

        [BsonIgnore]
        public Destination Destination { get; set; }
    }
}
