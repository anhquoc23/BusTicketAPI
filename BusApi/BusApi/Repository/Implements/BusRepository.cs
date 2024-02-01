using BusApi.Data;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repository.Implements
{
    public class BusRepository : IBusRepository
    {
        private readonly ApplicationDataContext _applicationDataContext;

        public BusRepository(ApplicationDataContext applicationDataContext)
        {  
            _applicationDataContext = applicationDataContext; 
        }

        public async Task<bool> AddOrUpdateBus(Bus bus)
        {
            try
            {
                var b = await this._applicationDataContext.Buses.FindAsync(bus.BusId);
                if (b == null)
                {
                    this._applicationDataContext.Add(bus);
                }
                else
                {
                    b.BusNumber = bus.BusNumber;
                    b.StationId = bus.StationId;
                }
                return await this._applicationDataContext.SaveChangesAsync() > 0;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBus(string id)
        {
            var bus = await this._applicationDataContext.Buses.FindAsync(id);
            if (bus != null)
            {
                bus.IsActive = false;
                this._applicationDataContext.Update(bus);
                return await this._applicationDataContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Bus>> GetBuses()
        {
            return await this._applicationDataContext.Buses.Include(b => b.BusStation).Where(b => b.IsActive).ToListAsync();
        }
    }
}
