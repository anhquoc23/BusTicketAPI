namespace BusApi.DTO
{
    public class TicketViewDTO
    {
        public int? TicketId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int TripId { get; set; }
        public int Seat { get; set; }
        public string BusId { get; set; }
    }
}
