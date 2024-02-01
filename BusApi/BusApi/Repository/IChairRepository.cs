using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Repository
{
    public interface IChairRepository
    {
        Task<bool> AddChairAuto(Bus bus, int? numberSeat);
        Task<IEnumerable<SeatViewDTO>> LoadSeatByTripId(int id);
        Task<Chair> GetSeatBySeatNumBerAndBusNumber(int seatNum, string busId);
    }
}
