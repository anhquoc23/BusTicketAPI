using BusApi.DTO;

namespace BusApi.Service
{
    public interface IStatService
    {
        IEnumerable<StatTripViewDTO> StatTripdensity(Dictionary<string, int> Statistics);
        Task<IEnumerable<StatRevenueViewDTO>> StatRevenue(Dictionary<string, int> Statistics);
    }
}
