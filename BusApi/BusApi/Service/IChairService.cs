using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface IChairService
    {
        Task<bool> AddChairAuto(Bus bus, int? numberSeat);
        Task<IEnumerable<SeatViewDTO>> LoadSeatByTripId(int id);
        Task<Chair> GetSeatBySeatNumBerAndBusNumber(int seatNum, string busId);
    }
}
