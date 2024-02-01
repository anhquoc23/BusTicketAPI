using BusApi.DTO;
using BusApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineService _routineService;
        public RoutineController(IRoutineService routineService)
        {
            _routineService = routineService;
        }
        [HttpGet("routine/list/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetRoutines()
        {
            var result = await this._routineService.GetRoutines();
            return Ok(result);
        }

        [HttpGet("routine/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetRouineById(int id)
        {
            var result = await this._routineService.GetRoutineById(id);
            return Ok(result);
        }

        [HttpPost("routine/add/")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddRoutine([FromForm] RoutineViewDTO routine)
        {
            if (ModelState.IsValid)
            {
                await this._routineService.AddOrUpdateRoutine(routine);
                return Ok("ADD SUCCESS");
            }
            return BadRequest(ModelState);
        }

        [HttpPut("routine/update/{id}/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateRoutine([FromBody] RoutineViewDTO? routine, int id)
        {
            if (routine.RoutineId != id)
            {
                ModelState.AddModelError("RoutineId", "Khong trung khop voi id cua routine va id trong url");
            }

            if (routine == null)
            {
                ModelState.AddModelError(string.Empty, "Json khong duoc null");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await this._routineService.AddOrUpdateRoutine(routine);
            if (result)
                return Ok(await this._routineService.GetRoutineById(id));
            ModelState.AddModelError(string.Empty, "Has An Error");
            return BadRequest(ModelState);
        }

        [HttpDelete("routine/delete/{id}/")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRoutine(int id)
        {
            var result = await this._routineService.DeleteRoutine(id);
            if (result)
                return NoContent();
            return BadRequest("Delete Failure");
        }
    }
}
