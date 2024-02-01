using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface IBusService
    {
        Task<IEnumerable<BusViewDTO>> GetBuses();
        Task<bool> AddOrUpdateBus(Bus bus);
        Task<bool> DeleteBus(string id);
    }
}
