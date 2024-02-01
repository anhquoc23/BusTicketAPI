using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            this._ticketRepository = ticketRepository;
        }

        public async Task<bool> addOrUpdateTicket(Ticket ticket)
        {
            return await this._ticketRepository.AddOrUpdateTicket(ticket);
        }

        public async Task<ExportTicketDTO> ExportTicket(Ticket ticket)
        {
            return await this._ticketRepository.ExportTicket(ticket);
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            return await this._ticketRepository.GetTicketById(id);
        }

        public async Task<IEnumerable<TicketInfoViewDTO>> GetTickets(Dictionary<string, string>? param = null)
        {
            return await this._ticketRepository.GetTickets(param);
        }
    }
}
