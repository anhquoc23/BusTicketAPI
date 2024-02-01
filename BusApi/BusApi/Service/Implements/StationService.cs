using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class StationService : IStationService
    {
        private readonly IStationRepository _stationRepository;
        public StationService(IStationRepository stationRepository)
        {
            this._stationRepository = stationRepository;
        }

        public async Task<bool> AddOrUpdateStation(Station station)
        {
            return await this._stationRepository.AddOrUpdateStation(station);
        }

        public Task<bool> DeleteStation(string id)
        {
            return this._stationRepository.DeleteStation(id);
        }

        public Task<IEnumerable<Station>> GetStation()
        {
            return this._stationRepository.GetStation();
        }

        public async Task<Station> GetStationById(string id)
        {
            return await this._stationRepository.GetStationById(id);
        }

        public Task<IEnumerable<Station>> getStationByKeyWord(string word)
        {
            return this._stationRepository.getStationByKeyWord(word);
        }
    }
}
