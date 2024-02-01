using BusApi.Configs;
using BusApi.Data;
using BusApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repository.Implements
{
    public class RoutineRepository : IRoutineRepository
    {
        private readonly ApplicationDataContext _applicationDataContext;
        public RoutineRepository(ApplicationDataContext applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
        }

        public async Task<bool> AddOrUpdateRoutine(Routine routine)
        {
            if (routine.Id != 0)
            {
                this._applicationDataContext.Routines.Update(routine);
            }
            else
            {
                this._applicationDataContext.Routines.Add(routine);
            }

            return await this._applicationDataContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountRoutines()
        {
            return await this._applicationDataContext.Routines.CountAsync(r => r.IsActive);
        }

        public async Task<bool> DeleteRoutine(int id)
        {
            var routine = await this._applicationDataContext.Routines.FindAsync(id);
            routine.IsActive = false;
            this._applicationDataContext.Update(routine);
            return await this._applicationDataContext.SaveChangesAsync() > 0;
        }

        public async Task<Routine> GetRoutineById(int id)
        {
            return await this._applicationDataContext.Routines.Include(r => r.StationStart).Include(r => r.StationEnd).FirstOrDefaultAsync(r => r.Id == id && r.IsActive);
        }

        public async Task<IEnumerable<Routine>> GetRoutines()
        {
            return await this._applicationDataContext.Routines.Include(r => r.StationStart).Include(r => r.StationEnd).Where(r => r.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<Routine>> GetRoutinesInPagination(int page)
        {
            return await this._applicationDataContext.Routines.Include(r => r.StationStart).Include(r => r.StationEnd).Where(r => r.IsActive).Skip((page - 1) * PageConfig.PAGE_SIZE).Take(PageConfig.PAGE_SIZE).ToListAsync();
        }

        public async Task<IEnumerable<Routine>> GetRoutinesByStation(string station)
        {
            var routines = await this._applicationDataContext.Routines.Include( r => r.StationStart)
                .Include(r => r.StationEnd).Where(r => r.IsActive &&
            (r.StationStart.StationName.ToLower().Contains(station.ToLower())) ||
            r.StationEnd.StationName.ToLower().Contains(station.ToLower())).ToListAsync();
            return routines;
        }

        public async Task<bool> CheckUniqueRoutine(string stationStart, string stationEnd)
        {
            var result = await this._applicationDataContext.Routines
                .AnyAsync(route => (route.StationStartId == stationStart &&
                          route.StationEndId == stationEnd) && route.IsActive);
            return result;
        }
    }
}
