using BusApi.Models;
using BusApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        private readonly IChairService _chairService;
        public BusController(IBusService busService, IChairService chairService)
        {
            _busService = busService;
            _chairService = chairService;
        }

        [HttpGet("bus/list/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBuses() 
        { 
            var result = await this._busService.GetBuses();
            return Ok(result);
        }

        [HttpPost("bus/add/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddBus([FromBody] Bus bus)
        {
            if (ModelState.IsValid)
            {
                var result = await this._busService.AddOrUpdateBus(bus);
                if (result)
                {
                    var resultChair = await _chairService.AddChairAuto(bus, bus.NumberSeat ?? 30);
                    if (resultChair) return Ok(bus);
                }
                else return BadRequest("Has An Error.");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("bus/update/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBus([FromBody] Bus? bus, string id)
        {
            if (id == null) return BadRequest("Id Is Not Null");
            if (bus.BusId != id) return BadRequest("Id Of Bus is difference with id input");
            if (bus == null) return BadRequest("Not Value in From Body");
            if (ModelState.IsValid)
            {
                var result = await this._busService.AddOrUpdateBus(bus);
                if (result)
                    return Ok(bus);
                else return BadRequest("Has An Error.");
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("bus/delete/{id}/")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBus(string id)
        {
            var result = await this._busService.DeleteBus(id);
            if (result) return NoContent();
            return BadRequest("Has An Error");
        }

        [HttpGet("seat/list")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetSeats(int tripId)
        {
            var result = await this._chairService.LoadSeatByTripId(tripId);
            return Ok(result);
        }

        [HttpGet("seat")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetSeats(int seatNumber, string busId)
        {
            var result = await this._chairService.GetSeatBySeatNumBerAndBusNumber(seatNumber, busId);
            return Ok(result);
        }
    }
}
