using BusApi.Models;
using BusApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }
        [HttpGet("comment/list/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Comments(int id)
        {
            var result = await _commentService.GetCommentsByTripId(id);
            return Ok(result);
        }

        [HttpPost("comment/add/{id}/")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddComment(int id, [FromQuery] string text)
        {
            var currentUser = await this._userService.GetUserByUsername(User.Identity.Name);
            Comment comment = new Comment
            {
                Content = text,
                TripId = id,
                CustomerId = currentUser.Id
            };
            var result = await this._commentService.AddComment(comment);
            if (result)
                 return Ok(comment);
            return BadRequest("Có lỗi xảy ra.!!!");
        }
    }
}
