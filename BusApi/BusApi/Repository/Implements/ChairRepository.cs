using BusApi.Data;
using BusApi.Data.Enum;
using BusApi.DTO;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace BusApi.Repository.Implements
{
    public class ChairRepository : IChairRepository
    {
        private readonly ApplicationDataContext _context;

        public ChairRepository(ApplicationDataContext context)
        {
            this._context = context;
        }
        public async Task<bool> AddChairAuto(Bus bus, int ? numberSeat)
        {
            for (int i = 0; i < numberSeat; i++)
            {
                var chair = new Chair
                {
                    ChairNumber = i + 1,
                    BusId = bus.BusId
                };
                this._context.Add(chair);
            }
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<Chair> GetSeatBySeatNumBerAndBusNumber(int seatNum, string busId)
        {
            var seat = await this._context.Chairs.Where(chair => chair.ChairNumber == seatNum && chair.BusId.Equals(busId)).FirstOrDefaultAsync();
            return seat;
        }

        public async Task<IEnumerable<SeatViewDTO>> LoadSeatByTripId(int id)
        {
            var seats = await this._context.Chairs.Where(s => s.IsActive)
                .Join(this._context.Buses, seat => seat.BusId, bus => bus.BusId,
                (seat, bus) => new
                {
                    Seat = seat,
                    Buses = bus
                }).Join(this._context.Trips.Where(t => t.TripId == id), s => s.Buses.BusId, trip => trip.BusId,
                (s, trip) => new
                {
                    SeatTrip = s.Seat
                }).GroupJoin(this._context.Tickets, s => s.SeatTrip.Id, tk => tk.SeatId, 
                (chair, ticket) => new SeatViewDTO
                {
                    SeatId = chair.SeatTrip.Id,
                    SeatNumber = chair.SeatTrip.ChairNumber,
                    Status = !ticket.Any() ? SeatStatus.AVAILABLE : SeatStatus.BOOKED
                }).ToListAsync();
            return seats;
        }
    }
}
