using BusApi.Data;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repository.Implements
{
    public class StationRepository : IStationRepository
    {
        private readonly ApplicationDataContext _context;
        public StationRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateStation(Station station)
        {
            var s = await this._context.Stations.FindAsync(station.StationId);
            try
            {
                if (s == null)
                {
                    this._context.Stations.Add(station);
                }
                else
                {
                    Console.WriteLine(station.StationId);
                    this._context.Entry(s).CurrentValues.SetValues(station);
                }
                return await this._context.SaveChangesAsync() > 0;
            } catch(DbUpdateException ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteStation(string id)
        {
            var station = await this._context.Stations.FindAsync(id);
            station.IsActive = true;
            this._context.Update(station);
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Station>> GetStation()
        {
            return await _context.Stations.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<Station> GetStationById(string id)
        {
            return await this._context.Stations.FindAsync(id);
        }

        public async Task<IEnumerable<Station>> getStationByKeyWord(string word)
        {
            string keyword = word.ToLower();
            var stations = await this._context.Stations.Where(s => s.IsActive && (s.StationName.ToLower().Contains(keyword) ||
            s.StationProvince.ToLower().Contains(keyword) || s.StationAddress.ToLower().Contains(keyword))).ToListAsync();
            return stations;
        }
    }
}
