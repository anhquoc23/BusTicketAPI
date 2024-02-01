using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Repository
{
    public interface ITicketRepository
    {
        Task<bool> AddOrUpdateTicket(Ticket ticket);
        Task<IEnumerable<TicketInfoViewDTO>> GetTickets(Dictionary<string, string>? param = null);
        Task<ExportTicketDTO> ExportTicket(Ticket ticket);
        Task<Ticket> GetTicketById(int id);
    }
}
