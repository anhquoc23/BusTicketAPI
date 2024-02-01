using BusApi.Data;
using BusApi.Data.Enum;
using BusApi.DTO;
using BusApi.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;

namespace BusApi.Repository.Implements
{
    public class StatRepository : IStatRepository
    {
        private readonly ApplicationDataContext _context;
        public StatRepository(ApplicationDataContext context) 
        { 
            this._context = context;
        }
        public async Task<IEnumerable<StatRevenueViewDTO>> StatRevenue(Dictionary<string, int> Statistics)
        {

            var result = this._context.Tickets.Where(x => x.Status.Equals(TicketStatus.CONFIRMED) && x.BookDate.Year == Statistics["Year"])
            .GroupBy(x => new
            {
                title = Statistics["Type"] == (int)TypeStat.YEAR ? x.BookDate.Year :
                        Statistics["Type"] == (int)TypeStat.MONTH ? x.BookDate.Month :
                        ((x.BookDate.Month - 1) / 3) + 1
            }).Select(x => new StatRevenueViewDTO
            {
                Title = x.Key.title,
                Revenue = (double)x.Sum(x => x.Price)
            });

            Console.WriteLine(result == null ? "null" : result.Count());
            return await result.ToListAsync();

        }

        public IEnumerable<StatTripViewDTO> StatTripdensity(Dictionary<string, int> Statistics)
        {
            IEnumerable<Trip> trips = this._context.Trips;
            switch (Statistics["Type"])
            {
                case (int)TypeStat.MONTH:
                    trips = trips.Where(t => t.TripDate.Month == Statistics["Month"] && t.TripDate.Year == Statistics["Year"]);
                    break;
                case (int)TypeStat.YEAR:
                    trips = trips.Where(t => t.TripDate.Year == Statistics["Year"]);
                    break;
                case (int)TypeStat.QUARTER:
                    trips = trips.Where(t => t.TripDate.Year == Statistics["Year"]);
                    break;
            }

            var routines = this._context.Routines.Include(r => r.StationStart).Include(r => r.StationEnd);

            var result = routines.ToList().Join(trips.ToList(),
                r => r.Id,
                t => t.RoutineId,
                (routine, trip) => new
                {
                    Routine = routine,
                    Trip = trip
                })
            .GroupBy(x => new
            {
                RoutineId = x.Routine.Id,
                Stationstart = x.Routine.StationStart,
                StationEnd = x.Routine.StationEnd,
                GroupTime = Statistics["Type"] == (int)TypeStat.YEAR ? x.Trip.TripDate.Year :
                            Statistics["Type"] == (int)TypeStat.MONTH ? x.Trip.TripDate.Month :
                            ((x.Trip.TripDate.Month - 1) / 3) + 1
            }).Select(group => new StatTripViewDTO
            {
                RoutineId = group.Key.RoutineId,
                RoutineName = string.Concat(group.Key.Stationstart.StationName, " - ", group.Key.StationEnd.StationName),
                Denisty = (group.Count() / (double)routines.Count(r => r.IsActive)) * 100
            });


            return result.ToList();
        }
    }
}
