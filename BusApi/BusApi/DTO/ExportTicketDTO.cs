namespace BusApi.DTO
{
    public class ExportTicketDTO
    {
        public string Route { get; set; }
        public DateTime DateTimeStart { get; set; }
        public decimal TotalPrice { get; set; }
        public int BusNumber { get; set; }
        public int SeatNumber {  get; set; }
        public string Customer { get; set; }
    }
}
