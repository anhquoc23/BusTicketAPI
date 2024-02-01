using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface ITripService
    {
        Task<IEnumerable<TripViewDTO>> getTrips(int pageNumber);
        Task<IEnumerable<TripViewDTO>> GetTripsByStartDate(DateTime startDate);
        Task<IEnumerable<TripViewDTO>> GetTripsByKeyWord(string keyword);
        Task<IEnumerable<TripViewDTO>> GetTripsByStatusAndKeyword(string status, string keyword = null);
        Task<bool> AddOrUpdateTrip(TripViewDTO trip);
        Task<TripViewDTO> GetTripById(int id);
        Task<bool> AddOrUpdateTrip(Trip trip);
        Task<bool> DeleteTrip(int id);
        Task<int> CountTrip();
        Task<int> CountTripsByStartDate(DateTime startDate);
        Task<int> CountTripsByKeyWord(string keyword);
        Task<int> CountTripsByStatusAndKeyword(string status, string keyword = null);
        Task<IEnumerable<TripViewDTO>> GetTripsByDriver(string driver);
        Task<bool> ChangeStatusTrip(string status, int tripId);
        Task<IEnumerable<TripViewDTO>> GetTripByStatus(DateTime date, string status);
    }
}
