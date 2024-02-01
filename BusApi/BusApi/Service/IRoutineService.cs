using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface IRoutineService
    {
        Task<IEnumerable<RoutineViewDTO>> GetRoutines();
        Task<IEnumerable<RoutineViewDTO>> GetRoutinesInPagination(int page);
        Task<int> CountRoutines();
        Task<IEnumerable<RoutineViewDTO>> GetRoutinesByStation(string station);

        Task<RoutineViewDTO> GetRoutineById(int id);
        Task<bool> AddOrUpdateRoutine(RoutineViewDTO routine);
        Task<bool> DeleteRoutine(int id);
        Task<bool> CheckUniqueRoutine(string stationStart, string stationEnd);
    }
}
