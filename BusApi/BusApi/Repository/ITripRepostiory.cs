using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Repository
{
    public interface ITripRepostiory
    {
        Task<IEnumerable<Trip>> GetTrips(int pageNumber);
        Task<IEnumerable<Trip>> GetTripsByStartDate(DateTime startDate);
        Task<IEnumerable<Trip>> GetTripsByKeyWord(string keyword);
        Task<IEnumerable<Trip>> GetTripsByStatusAndKeyword(string status, string keyword = null);
        Task<bool> AddOrUpdateTrip(Trip trip);
        Task<Trip> GetTripById(int id);
        Task<bool> DeleteTrip(int id);
        Task<int> CountTrip();
        Task<int> CountTripsByStartDate(DateTime startDate);
        Task<int> CountTripsByKeyWord(string keyword);
        Task<int> CountTripsByStatusAndKeyword(string status, string keyword = null);
        Task<IEnumerable<Trip>> GetTripsByDriver(string driver);
        Task<bool> ChangeStatusTrip(string status, int tripId);
        Task<IEnumerable<Trip>> GetTripByStatus(DateTime date, string status);
    }
}
