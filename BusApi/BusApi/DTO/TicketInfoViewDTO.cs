namespace BusApi.DTO
{
    public class TicketInfoViewDTO
    {
        public int TicketId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime BookDate { get; set; }
        public int TripId { get; set; }
        public int TotalSeat { get; set; }
        public decimal TotalPrice { get; set; }
        public int BusNumber { get; set; }
    }
}
