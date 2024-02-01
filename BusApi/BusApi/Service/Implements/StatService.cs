using BusApi.DTO;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class StatService : IStatService
    {
        private readonly IStatRepository _statRepository;
        public StatService(IStatRepository statRepository) 
        { 
            this._statRepository = statRepository;
        }
        public async Task<IEnumerable<StatRevenueViewDTO>> StatRevenue(Dictionary<string, int> Statistics)
        {
            return await this._statRepository.StatRevenue(Statistics);
        }

        public IEnumerable<StatTripViewDTO> StatTripdensity(Dictionary<string, int> Statistics)
        {
            return this._statRepository.StatTripdensity(Statistics);
        }
    }
}
