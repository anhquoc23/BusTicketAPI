using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Repository
{
    public interface IStatRepository
    {
        IEnumerable<StatTripViewDTO> StatTripdensity(Dictionary<string, int> Statistics);
        Task<IEnumerable<StatRevenueViewDTO>> StatRevenue(Dictionary<string, int> Statistics);
    }
}
