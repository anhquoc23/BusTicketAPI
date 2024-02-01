using BusApi.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace BusApi.DTO
{
    public class TripViewDTO
    {
        public int TripId { get; set; }
        public DateTime TripDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public Decimal UnitPrice { get; set; }
        public string? DriverName { get; set; }
        public string DriverId { get; set; }
        public int? BusNumber { get; set; }
        public string BusId { get; set; }
        public string? Routine { get; set; }
        public int RoutineId { get; set; }
        public string Status { get; set; } = TripStatus.WAITTING;
        public int Distance { get; set; }
    }
}
