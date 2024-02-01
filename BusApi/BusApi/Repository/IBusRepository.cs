using BusApi.Models;

namespace BusApi.Repository
{
    public interface IBusRepository
    {
        Task<IEnumerable<Bus>> GetBuses();
        Task<bool> AddOrUpdateBus(Bus bus);
        Task<bool> DeleteBus(string id);
    }
}
