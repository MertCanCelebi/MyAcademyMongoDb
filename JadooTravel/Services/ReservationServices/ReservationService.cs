using AutoMapper;
using JadooTravel.Dtos.ReservationDtos;
using JadooTravel.Entities;
using JadooTravel.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace JadooTravel.Services.ReservationServices
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Destination> _destinationCollection;
        private readonly IMapper _mapper;

        public ReservationService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reservationCollection = database.GetCollection<Reservation>(databaseSettings.ReservationCollectionName);
            _destinationCollection = database.GetCollection<Destination>(databaseSettings.DestinationCollectionName);
            _mapper = mapper;
        }
        public async Task CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(createReservationDto);
            await _reservationCollection.InsertOneAsync(reservation);
        }

        public async Task DeleteReservationAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(x => x.ReservationId == id);
        }

        public async Task<List<ResultReservationDto>> GetAllReservationAsync()
        {
            var reservations = await _reservationCollection.Find(_ => true).ToListAsync();
            var resultList = new List<ResultReservationDto>();

            foreach (var r in reservations)
            {
                var dto = _mapper.Map<ResultReservationDto>(r);

                // Destination bilgisi doldur
                var destination = await _destinationCollection
                    .Find(d => d.DestinationId == r.DestinationId)
                    .FirstOrDefaultAsync();

                dto.DestinationCityCountry = destination?.CityCountry;

                resultList.Add(dto);
            }

            return resultList;
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(string id)
        {
            var reservation = await _reservationCollection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();
            if (reservation == null) return null;

            var dto = _mapper.Map<GetReservationByIdDto>(reservation);

            var destination = await _destinationCollection
                .Find(d => d.DestinationId == reservation.DestinationId)
                .FirstOrDefaultAsync();

            if (destination != null)
            {
                dto.DestinationCityCountry = destination.CityCountry;
            }

            return dto;
        }

        public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(updateReservationDto);

            var update = Builders<Reservation>.Update
                .Set(x => x.NameSurname, reservation.NameSurname)
                .Set(x => x.PhoneNumber, reservation.PhoneNumber)
                .Set(x => x.Email, reservation.Email)
                .Set(x => x.Note, reservation.Note)
                .Set(x => x.DestinationId, reservation.DestinationId);

            await _reservationCollection.UpdateOneAsync(
                x => x.ReservationId == updateReservationDto.ReservationId, update);
        }
    }
}
