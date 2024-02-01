using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class ChairService : IChairService
    {
        private readonly IChairRepository _seatRepository;
        public ChairService(IChairRepository seatRepository)
        {
            this._seatRepository = seatRepository;
        }

        public async Task<bool> AddChairAuto(Bus bus, int? numberSeat)
        {
            return await this._seatRepository.AddChairAuto(bus, numberSeat);
        }

        public async Task<Chair> GetSeatBySeatNumBerAndBusNumber(int seatNum, string busId)
        {
            return await this._seatRepository.GetSeatBySeatNumBerAndBusNumber(seatNum, busId);
        }

        public async Task<IEnumerable<SeatViewDTO>> LoadSeatByTripId(int id)
        {
            return await this._seatRepository.LoadSeatByTripId(id);
        }
    }
}
