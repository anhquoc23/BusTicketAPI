
using BusApi.Data.Enum;
using BusApi.DTO;
using BusApi.Models;
using BusApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BusApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        public TripController(ITripService tripService) 
        { 
            this._tripService = tripService;
        }

        [HttpGet("trip/list/")]
        [ProducesResponseType(statusCode: 200, Type = typeof(TripViewDTO))]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Trips(int page = 1, string keyword = null, string dateQuery = null)
        {
            var trips = await this._tripService.getTrips(page);
            if (!keyword.IsNullOrEmpty())
            {
                trips = await this._tripService.GetTripsByKeyWord(keyword);
            }
            else if (!dateQuery.IsNullOrEmpty())
            {
                trips = await this._tripService.GetTripsByStartDate(DateTime.Parse(dateQuery));
            }
            else
                trips = await this._tripService.getTrips(page);
            return Ok(trips);
        }

        [HttpGet("trip/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTripById(int id)
        {
            var trip = await this._tripService.GetTripById(id);
            return trip != null ? Ok(trip) : BadRequest("No Exist In Database");
        }

        [HttpPost("trip")]
        public async Task<IActionResult> AddTrip([FromBody] TripViewDTO? tripView)
        {
            tripView.Status = TripStatus.WAITTING;
            List<string> errors = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (var model in ModelState.Values)
                {
                    foreach (var error in model.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
            }
            var result = await this._tripService.AddOrUpdateTrip(tripView);
            return result ? Ok("ADD TRIP SUCCESS") : BadRequest(errors);
        }

        [HttpPut("trip/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateTrip([FromBody] TripViewDTO? tripView, int id)
        {
            if(id != tripView.TripId)
                return BadRequest(ModelState);
            if (tripView == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await this._tripService.AddOrUpdateTrip(tripView);
            return result ? Ok("SUccess") : BadRequest("Failure");
        }

        [HttpDelete("trip/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteTrip(int id = 0)
        {
            if (id <= 0)
                return BadRequest("TripId Not Null Or Less Or Equal 0");
            var result = await this._tripService.DeleteTrip(id);
            return result ? NoContent() : BadRequest("Delete Failure");
        }
    }
}
