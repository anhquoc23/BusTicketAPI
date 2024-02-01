using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class RoutineService : IRoutineService
    {
        private readonly IRoutineRepository _routineRepository;
        public RoutineService(IRoutineRepository routineRepository)
        {
            _routineRepository = routineRepository;
        }

        public async Task<bool> AddOrUpdateRoutine(RoutineViewDTO routine)
        {
            Console.WriteLine($"---service: {routine.Distance}");
            return await this._routineRepository.AddOrUpdateRoutine(new Routine
            {
                Id = routine.RoutineId ?? 0,
                StationStartId = routine.StationStartId,
                StationEndId = routine.StationEndId,
                Distance = routine.Distance,
                IsActive = true
            });
        }

        public Task<bool> CheckUniqueRoutine(string stationStart, string stationEnd)
        {
            return this._routineRepository.CheckUniqueRoutine(stationStart, stationEnd);
        }

        public async Task<int> CountRoutines()
        {
            return await _routineRepository.CountRoutines();
        }

        public async Task<bool> DeleteRoutine(int id)
        {
            return await this._routineRepository.DeleteRoutine(id);
        }

        public async Task<RoutineViewDTO> GetRoutineById(int id)
        {
            var r = await this._routineRepository.GetRoutineById(id);
            if (r == null) return null;
            return new RoutineViewDTO
            {
                RoutineId = r.Id,
                Distance = r.Distance,
                StationStartId = r.StationStartId,
                StationNameStart = r.StationStart.StationName,
                StationProvinceStart = r.StationStart.StationProvince,
                StationAddressStart = r.StationStart.StationAddress,
                StationEndId = r.StationEndId,
                StationNameEnd = r.StationEnd.StationName,
                StationProvinceEnd = r.StationEnd.StationProvince,
                StationAddressEnd = r.StationEnd.StationAddress,
            };
        }

        public async Task<IEnumerable<RoutineViewDTO>> GetRoutines()
        {
            var routines = await this._routineRepository.GetRoutines();
            return routines.Select(r => new RoutineViewDTO()
            {
                RoutineId = r.Id,
                Distance = r.Distance,
                StationStartId = r.StationStartId,
                StationNameStart = r.StationStart.StationName,
                StationProvinceStart = r.StationStart.StationProvince,
                StationAddressStart = r.StationStart.StationAddress,
                StationEndId = r.StationEndId,
                StationNameEnd = r.StationEnd.StationName,
                StationProvinceEnd = r.StationEnd.StationProvince,
                StationAddressEnd = r.StationEnd.StationAddress,
            }).ToList();
        }

        public async Task<IEnumerable<RoutineViewDTO>> GetRoutinesByStation(string station)
        {
            var routines = await this._routineRepository.GetRoutinesByStation(station);
            return routines.Select(r => new RoutineViewDTO()
            {
                RoutineId = r.Id,
                Distance = r.Distance,
                StationStartId = r.StationStartId,
                StationNameStart = r.StationStart.StationName,
                StationProvinceStart = r.StationStart.StationProvince,
                StationAddressStart = r.StationStart.StationAddress,
                StationEndId = r.StationEndId,
                StationNameEnd = r.StationEnd.StationName,
                StationProvinceEnd = r.StationEnd.StationProvince,
                StationAddressEnd = r.StationEnd.StationAddress
            }).ToList();
        }

        public async Task<IEnumerable<RoutineViewDTO>> GetRoutinesInPagination(int page)
        {
            var routines = await this._routineRepository.GetRoutinesInPagination(page);
            return routines.Select(r => new RoutineViewDTO()
            {
                RoutineId = r.Id,
                Distance = r.Distance,
                StationStartId = r.StationStartId,
                StationNameStart = r.StationStart.StationName,
                StationProvinceStart = r.StationStart.StationProvince,
                StationAddressStart = r.StationStart.StationAddress,
                StationEndId = r.StationEndId,
                StationNameEnd = r.StationEnd.StationName,
                StationProvinceEnd = r.StationEnd.StationProvince,
                StationAddressEnd = r.StationEnd.StationAddress
            }).ToList();
        }
    }
}
