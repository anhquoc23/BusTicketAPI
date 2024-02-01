using BusApi.Models;
using BusApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BusApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationService _stationService;
        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }
        [HttpGet("station/list/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStations([FromQuery(Name = "keyword")] string? keyWord)
        {
            if (!keyWord.IsNullOrEmpty())
            {
                return Ok(await this._stationService.getStationByKeyWord(keyWord));
            }
            return Ok(await this._stationService.GetStation());
        }

        [HttpGet("station/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStationById(string id)
        {
            var result = await this._stationService.GetStationById(id);
            return Ok(result);
        }

        [HttpPost("station/add/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddStation([FromBody] Station station)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await this._stationService.AddOrUpdateStation(station);
            return Ok(new { Message = "Add Success", Product = station });
        }

        [HttpPut("station/update/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateStation([FromBody] Station? station, string id)
        {
            if (station == null)
            {
                ModelState.AddModelError(string.Empty, "Doi tuong khong duoc null");
                return BadRequest(ModelState);
            }    

            if (station.StationId != id)
            {
                ModelState.AddModelError("StationId", "Id cua doi tuong khac voi id truyen vao");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await this._stationService.AddOrUpdateStation(station);
            if (result)
                return Ok(station);
            return BadRequest("Update Failure");
        }

        [HttpDelete("station/delete/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteStation(string id)
        {
            var result = await this._stationService.DeleteStation(id);
            if (result)
                return Ok("Delete Success");
            return BadRequest("Delete Failure");
        }

    }
}
