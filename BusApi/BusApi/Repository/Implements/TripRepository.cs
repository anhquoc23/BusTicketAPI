using BusApi.Configs;
using BusApi.Data;
using BusApi.Data.Enum;
using BusApi.DTO;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BusApi.Repository.Implements
{
    public class TripRepository : ITripRepostiory
    {
        private readonly ApplicationDataContext _context;

        public TripRepository(ApplicationDataContext context)
        {
            this._context = context;
        }

        public async Task<bool> AddOrUpdateTrip(Trip trip)
        {
            if (trip.TripId > 0)
                this._context.Update(trip);
            else
                this._context.Add(trip);
            var rows = await this._context.SaveChangesAsync();
            return rows > 0 ? true : false;
        }

        public async Task<bool> ChangeStatusTrip(string status, int tripId)
        {
            var trip = await this._context.Trips.FindAsync(tripId);
            trip.Status = status;
            this._context.Trips.Update(trip);
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountTrip()
        {
            return await this._context.Trips.CountAsync(t => t.IsActive);
        }

        public async Task<int> CountTripsByKeyWord(string keyword)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(trip => trip.IsActive && trip.Status.Equals(TripStatus.WAITTING) &&
        (trip.RouteTrip.StationStart.StationName.ToLower().Contains(keyword) || trip.RouteTrip.StationEnd.StationName.ToLower().Contains(keyword)));
            return await trips.CountAsync();
        }

        public async Task<int> CountTripsByStartDate(DateTime startDate)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(trip => trip.IsActive && trip.Status.Equals(TripStatus.WAITTING) && trip.TripDate.Date == startDate.Date);
            return await trips.CountAsync();
        }

        public async Task<int> CountTripsByStatusAndKeyword(string status, string keyword = null)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(trip => trip.IsActive && trip.Status == status && keyword.IsNullOrEmpty() ? true :
        (trip.RouteTrip.StationStart.StationName.ToLower().Contains(keyword) || trip.RouteTrip.StationEnd.StationName.ToLower().Contains(keyword)));
            return await trips.CountAsync();
        }

        public async Task<bool> DeleteTrip(int id)
        {
            var trip = await this._context.Trips.FindAsync(id);
            trip.IsActive = false;
            this._context.Update(trip);
            return await this._context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<Trip> GetTripById(int id)
        {
            return await this._context.Trips.Include(t => t.RouteTrip).ThenInclude(b => b.StationStart)
                .Include(t => t.RouteTrip).ThenInclude(b => b.StationEnd).Include(t => t.BusTrip).Include(d => d.Driver).FirstOrDefaultAsync(trip => trip.IsActive && trip.TripId == id);
        }

        public async Task<IEnumerable<Trip>> GetTripByStatus(DateTime date, string status)
        {
            Console.WriteLine(date.Date);
            Console.WriteLine(status);
            return await this._context.Trips.Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(t => t.TripDate.Date == date.Date && t.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTrips(int pageNumber)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Skip(PageConfig.PAGE_SIZE * (pageNumber - 1)).Take(PageConfig.PAGE_SIZE).Where(trip => trip.IsActive && trip.Status.Equals(TripStatus.WAITTING)).OrderBy(trip => trip.TripDate);
            return await trips.ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsByDriver(string driver)
        {
            var result = await this._context.Trips.Include(t => t.BusTrip).Include(t => t.Driver).Include(t => t.RouteTrip)
                .ThenInclude(t => t.StationStart).Include(t => t.RouteTrip).ThenInclude(t => t.StationEnd)
                .Where(t => t.DriverId == driver && !t.Status.Equals(TripStatus.COMPLETED)).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Trip>> GetTripsByKeyWord(string keyword)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(trip => trip.IsActive && trip.Status.Equals(TripStatus.WAITTING) &&
        (trip.RouteTrip.StationStart.StationName.ToLower().Contains(keyword) || trip.RouteTrip.StationEnd.StationName.ToLower().Contains(keyword)));
            return await trips.ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsByStartDate(DateTime startDate)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(trip => trip.IsActive && trip.Status.Equals(TripStatus.WAITTING) && trip.TripDate.Date == startDate.Date);
            return await trips.ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsByStatusAndKeyword(string status, string keyword = null)
        {
            var trips = this._context.Trips
    .Include(d => d.Driver)
    .Include(d => d.BusTrip)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationStart)
    .Include(d => d.RouteTrip)
        .ThenInclude(rt => rt.StationEnd).Where(trip => trip.IsActive && trip.Status == status && keyword.IsNullOrEmpty() ? true :
        (trip.RouteTrip.StationStart.StationName.ToLower().Contains(keyword) || trip.RouteTrip.StationEnd.StationName.ToLower().Contains(keyword)));
            return await trips.ToListAsync();
        }
    }
}
