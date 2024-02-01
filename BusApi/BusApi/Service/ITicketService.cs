using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface ITicketService
    {
        Task<bool> addOrUpdateTicket(Ticket ticket);
        Task<IEnumerable<TicketInfoViewDTO>> GetTickets(Dictionary<string, string>? param);
        Task<ExportTicketDTO> ExportTicket(Ticket ticket);
        Task<Ticket> GetTicketById(int id);
    }
}
