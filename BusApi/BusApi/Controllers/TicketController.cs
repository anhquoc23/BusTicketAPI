using BusApi.DTO;
using BusApi.Models;
using BusApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ITripService _tripService;
        private readonly IUserService _userService;
        public TicketController(ITicketService ticketService, ITripService tripService, IUserService userService)
        {
            _ticketService = ticketService;
            _tripService = tripService;
            _userService = userService;
        }

        [HttpGet("ticket/list/")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Tickets(string? date, string? keyword)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (date != null ) param.Add("Date", date);
            else param.Add("Keyword", keyword);
            if (date == null && keyword == null)
            {
                param = null;
                Console.WriteLine("Check");
            }
            var result = await _ticketService.GetTickets(param);
            return Ok(result);
        }

        [HttpGet("ticket/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var result = await _ticketService.GetTicketById(id); return Ok(result);
        }

        [HttpPost("ticket/add/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddTicket([FromForm] TicketViewDTO ticketView)
        {
            var user = User.Identity.Name;
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var trip  = await this._tripService.GetTripById(ticketView.TripId);
            var currentUser = await this._userService.GetUserByUsername(user);
            Ticket ticket = new Ticket
            {
                BookDate = trip.TripDate,
                Price = trip.UnitPrice,
                FullName = user != null ? $"{currentUser.LastName} {currentUser.FirstName}" : ticketView.FullName,
                Email = user != null ? currentUser.Email : ticketView.Email,
                PhoneNumber = user != null ? currentUser.PhoneNumber : ticketView.Phone,
                SeatId = ticketView.Seat,
                TripId = ticketView.TripId
            };
            var result = await this._ticketService.addOrUpdateTicket(ticket);
            if (result)
                return Ok("Thêm Vé Thành Công");
            return BadRequest("Có lỗi xảy ra vui lòng thử lại");
        }
    }
}
