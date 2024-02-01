using BusApi.Data;
using BusApi.DTO;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BusApi.Repository.Implements
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDataContext _context;
        public TicketRepository(ApplicationDataContext context) 
        {
            this._context = context;
        }
        public async Task<bool> AddOrUpdateTicket(Ticket ticket)
        {
            var tkt = await this._context.Tickets.Where(tkt => tkt.TicketId == ticket.TicketId).AnyAsync();
            if (tkt)
            {
                this._context.Update(ticket);
            }
            else
                this._context.Tickets.Add(ticket);
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<ExportTicketDTO> ExportTicket(Ticket ticket)
        {
            var result = await this._context.Tickets
                .Include(tkt => tkt.Trip).ThenInclude(t => t.RouteTrip).ThenInclude(r => r.StationStart)
                .Include(tkt => tkt.Trip).ThenInclude(t => t.RouteTrip).ThenInclude(r => r.StationEnd)
                .Include(tkt => tkt.Trip).ThenInclude(b => b.BusTrip)
                .Include(tkt => tkt.Seat)
                .Where(tkt => tkt.FullName == ticket.FullName && tkt.TripId == ticket.TripId)
                .Select(r => new ExportTicketDTO
                {
                    Route = string.Concat(r.Trip.RouteTrip.StationStart.StationName, " - ", r.Trip.RouteTrip.StationEnd.StationName),
                    DateTimeStart = r.BookDate,
                    Customer = r.FullName,
                    BusNumber = r.Trip.BusTrip.BusNumber,
                    TotalPrice = r.Price,
                    SeatNumber = r.Seat.ChairNumber
                }).FirstOrDefaultAsync();
            //var result = await this._context.Tickets.Where(tkt => tkt.FullName == ticket.FullName && tkt.TripId == ticket.TripId)
            //    .GroupBy(t => new
            //    {
            //        Route = string.Concat(t.Trip.RouteTrip.StationStart.StationName, " -> ", t.Trip.RouteTrip.StationEnd.StationName),
            //        DateTimeStart = t.BookDate,
            //        FullName = t.FullName,
            //        BusName = t.Trip.BusTrip.BusNumber,
            //        SeatNumber =
            //    }).Select(tk => new ExportTicketDTO
            //    {
            //        Route = tk.Key.Route,
            //        DateTimeStart = tk.Key.DateTimeStart,
            //        Customer = tk.Key.FullName,
            //        BusNumber = tk.Key.BusName,
            //        TotalPrice = tk.Sum(x => x.SeatId * x.Price),
            //        SeatNumber = tk.
            //    }).FirstOrDefaultAsync();

            // Lấy danh sách ghế chọn
            //var seats  = await this._context.Tickets.Where(t => t.TripId == ticket.TripId && t.FullName == ticket.FullName).Select(x => new Chair { Id= x.Seat.Id,ChairNumber = x.Seat.ChairNumber}).ToListAsync();
            //ICollection<int> seatListSelected = new List<int>();
            //foreach (var s in seats)
            //{
            //    result.SeatNumbers.Add(s.ChairNumber);
            //}
            return result;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await this._context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<TicketInfoViewDTO>> GetTickets(Dictionary<string, string>? param)
        {
            IQueryable<Ticket> query = this._context.Tickets;

            if (param != null)
            {
                if (param.ContainsKey("Date"))
                {
                    query = query.Where(x => x.CreatedDate.Date == DateTime.ParseExact(param["Date"], "yyyy-MM-dd", CultureInfo.InvariantCulture).Date);
                }
                else if (param.ContainsKey("Keyword"))
                {
                    var word = param["Keyword"].ToLower();
                    query = query.Where(x => x.FullName.ToLower().Contains(word) || x.Email.Contains(word) || x.PhoneNumber.Contains(word));
                }
            }

            var tickets = query.Include(x => x.Seat)
                .Include(tkt => tkt.Trip).ThenInclude(b => b.BusTrip)
                .Select(t => new TicketInfoViewDTO
            {
                FullName = t.FullName, Email = t.Email, PhoneNumber = t.PhoneNumber,
                BookDate = t.BookDate, CreatedDate = t.CreatedDate, TripId = t.TripId,
                TotalSeat = t.Seat.ChairNumber,
                TotalPrice = t.Price,
                BusNumber = t.Trip.BusTrip.BusNumber,
                TicketId = t.TicketId
            }).OrderBy(x => x.CreatedDate);
            return await tickets.ToListAsync();
        }
    }
}
