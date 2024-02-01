namespace BusApi.DTO
{
    public class RoutineViewDTO
    {
        public int? RoutineId { get; set; }
        public int Distance { get; set; }
        public string StationStartId { get; set; }
        public string? StationNameStart { get; set; }
        public string? StationProvinceStart { get; set; }
        public string? StationAddressStart { get; set; }
        public string StationEndId { get; set; }
        public string? StationNameEnd { get; set; }
        public string? StationProvinceEnd { get; set; }
        public string? StationAddressEnd { get; set; }
    }
}
