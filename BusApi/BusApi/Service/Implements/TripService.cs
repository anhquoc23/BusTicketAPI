using BusApi.Data.Enum;
using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;
using BusApi.Repository.Implements;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BusApi.Service.Implements
{
    public class TripService : ITripService
    {
        private readonly ITripRepostiory _tripRepository;
        
        public TripService(ITripRepostiory tripRepository)
        {
            this._tripRepository = tripRepository;
        }

        public async Task<bool> AddOrUpdateTrip(TripViewDTO trip)
        {
            Trip t = new Trip()
            {
                TripDate = trip.TripDate,
                StartTime = trip.StartTime,
                UnitPrice = trip.UnitPrice,
                Status = trip.Status ?? TripStatus.WAITTING,
                DriverId = trip.DriverId,
                BusId = trip.BusId,
                RoutineId = trip.RoutineId,
                TripId = trip.TripId,
            };
            return await this._tripRepository.AddOrUpdateTrip(t);
        }

        public Task<bool> AddOrUpdateTrip(Trip trip)
        {
            return this._tripRepository.AddOrUpdateTrip(trip);
        }

        public async Task<bool> ChangeStatusTrip(string status, int tripId)
        {
            return await this._tripRepository.ChangeStatusTrip(status, tripId);
        }

        public async Task<int> CountTrip()
        {
            return await this._tripRepository.CountTrip();
        }

        public async Task<int> CountTripsByKeyWord(string keyword)
        {
            return await this._tripRepository.CountTripsByKeyWord(keyword);
        }

        public Task<int> CountTripsByStartDate(DateTime startDate)
        {
            return this._tripRepository.CountTripsByStartDate(startDate);
        }

        public Task<int> CountTripsByStatusAndKeyword(string status, string keyword = null)
        {
            return this._tripRepository.CountTripsByStatusAndKeyword(status, keyword);
        }

        public async Task<bool> DeleteTrip(int id)
        {
            return await this._tripRepository.DeleteTrip(id);
        }

        public async Task<TripViewDTO> GetTripById(int id)
        {
            var trip = await this._tripRepository.GetTripById(id);
            return trip == null ? null : new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                UnitPrice = trip.UnitPrice,
                DriverName = $"{trip.Driver.LastName} {trip.Driver.FirstName}",
                DriverId = trip.DriverId,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = $"{trip.RouteTrip.StationStart.StationName} - {trip.RouteTrip.StationEnd.StationName}",
                Status = trip.Status,
                StartTime = trip.StartTime,
                Distance = trip.RouteTrip.Distance,
                RoutineId= trip.RoutineId,
                BusId = trip.BusId
            };
        }

        public async Task<IEnumerable<TripViewDTO>> GetTripByStatus(DateTime date, string status)
        {
            var result = await this._tripRepository.GetTripByStatus(date, status);
            return result.Select(trip => new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                UnitPrice = trip.UnitPrice,
                DriverName = $"{trip.Driver.LastName} {trip.Driver.FirstName}",
                DriverId = trip.DriverId,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = $"{trip.RouteTrip.StationStart.StationName} - {trip.RouteTrip.StationEnd.StationName}",
                StartTime = trip.StartTime,
                Distance = trip.RouteTrip.Distance
            });
        }

        public async Task<IEnumerable<TripViewDTO>> getTrips(int pageNumber)
        {
            var trips = await this._tripRepository.GetTrips(pageNumber);
            var tripViews = trips.Select(trip => new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                UnitPrice = trip.UnitPrice,
                DriverName = $"{trip.Driver.LastName} {trip.Driver.FirstName}",
                DriverId = trip.DriverId,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = $"{trip.RouteTrip.StationStart.StationName} - {trip.RouteTrip.StationEnd.StationName}",
                Status = trip.Status,
                StartTime = trip.StartTime,
                Distance = trip.RouteTrip.Distance
            });

            return tripViews;
        }

        public async Task<IEnumerable<TripViewDTO>> GetTripsByDriver(string driver)
        {
            var result = await this._tripRepository.GetTripsByDriver(driver);
            var trips = result.Select(trip => new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                StartTime = trip.StartTime,
                UnitPrice = trip.UnitPrice,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = trip.RouteTrip.StationStart.StationName + " -> " + trip.RouteTrip.StationEnd.StationName,
                Distance = trip.RouteTrip.Distance,
                Status = trip.Status
            }).ToList();
            return trips;
        }

        public async Task<IEnumerable<TripViewDTO>> GetTripsByKeyWord(string keyword)
        {
            var trips = await this._tripRepository.GetTripsByKeyWord(keyword);
            var tripViews = trips.Select(trip => new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                UnitPrice = trip.UnitPrice,
                DriverName = $"{trip.Driver.LastName} {trip.Driver.FirstName}",
                DriverId = trip.DriverId,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = $"{trip.RouteTrip.StationStart.StationName} - {trip.RouteTrip.StationEnd.StationName}",
                Status = trip.Status,
                StartTime = trip.StartTime,
                Distance = trip.RouteTrip.Distance
            }).ToList();

            return tripViews;
        }

        public async Task<IEnumerable<TripViewDTO>> GetTripsByStartDate(DateTime startDate)
        {
            var trips = await this._tripRepository.GetTripsByStartDate(startDate);
            var tripViews = trips.Select(trip => new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                UnitPrice = trip.UnitPrice,
                DriverName = $"{trip.Driver.LastName} {trip.Driver.FirstName}",
                DriverId = trip.DriverId,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = $"{trip.RouteTrip.StationStart.StationName} - {trip.RouteTrip.StationEnd.StationName}",
                Status = trip.Status,
                StartTime = trip.StartTime,
                Distance = trip.RouteTrip.Distance
            });

            return tripViews;
        }

        public async Task<IEnumerable<TripViewDTO>> GetTripsByStatusAndKeyword(string status, string keyword = null)
        {
            var trips = await this._tripRepository.GetTripsByStatusAndKeyword(status, keyword);
            var tripViews = trips.Select(trip => new TripViewDTO
            {
                TripId = trip.TripId,
                TripDate = trip.TripDate,
                UnitPrice = trip.UnitPrice,
                DriverName = $"{trip.Driver.LastName} {trip.Driver.FirstName}",
                DriverId = trip.DriverId,
                BusNumber = trip.BusTrip.BusNumber,
                Routine = $"{trip.RouteTrip.StationStart.StationName} - {trip.RouteTrip.StationEnd.StationName}",
                Status = trip.Status,
                StartTime = trip.StartTime,
                Distance = trip.RouteTrip.Distance
            });

            return tripViews;
        }
    }
}
