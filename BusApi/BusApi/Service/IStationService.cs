using BusApi.Models;

namespace BusApi.Service
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetStation();
        Task<Station> GetStationById(string id);
        Task<bool> AddOrUpdateStation(Station station);
        Task<bool> DeleteStation(string id);
        Task<IEnumerable<Station>> getStationByKeyWord(string word);
    }
}
