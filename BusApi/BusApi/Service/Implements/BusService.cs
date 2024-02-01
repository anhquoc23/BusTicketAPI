using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        public BusService(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public async Task<bool> AddOrUpdateBus(Bus bus)
        {
            return await this._busRepository.AddOrUpdateBus(bus);
        }

        public async Task<bool> DeleteBus(string id)
        {
            return await this._busRepository.DeleteBus(id);
        }

        public async Task<IEnumerable<BusViewDTO>> GetBuses()
        {
            var buses = await this._busRepository.GetBuses();
            var bus = buses.Select(b => new BusViewDTO
            {
                BusId = b.BusId,
                BusNumber = b.BusNumber,
                StationName = b.BusStation.StationName,
            });
            return bus.ToList();
        }
    }
}
