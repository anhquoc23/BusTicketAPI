using BusApi.Models;

namespace BusApi.Repository
{
    public interface IRoutineRepository
    {
        Task<IEnumerable<Routine>> GetRoutines();
        Task<IEnumerable<Routine>> GetRoutinesInPagination(int page);
        Task<int> CountRoutines();
        Task<IEnumerable<Routine>> GetRoutinesByStation(string station); 
        Task<Routine> GetRoutineById(int id);
        Task<bool> AddOrUpdateRoutine(Routine routine);
        Task<bool> DeleteRoutine(int id);
        Task<bool> CheckUniqueRoutine(string stationStart, string stationEnd);
    }
}
