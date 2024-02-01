using BusApi.Authenticate;
using BusApi.Data.Enum;
using BusApi.DTO;
using BusApi.Models;
using BusApi.Service;
using BusApi.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BusApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICloudinarySetting _cloudinarySetting;
        public UserController(ITokenService tokenService, IUserService userService, 
            SignInManager<User> signInManager, UserManager<User> userManager, ICloudinarySetting cloudinarySetting)
        {
            _tokenService = tokenService;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _cloudinarySetting = cloudinarySetting;
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginDTO? loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (loginDto == null) return Unauthorized("Invalid Username Or Password");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Username", "admin");
            var user = await this._userService.GetUserInfoByKeyWord(dict);
            if (user == null) return Unauthorized("Not Exist Username. Please Try Agian.");
            Console.WriteLine(user.Id);
            Console.WriteLine(user.PasswordHash);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.PhoneNumber);
            var result = await this._signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, isPersistent:true, lockoutOnFailure:false);
            if (result.Succeeded) return Ok(_tokenService.CreateToken(user));
            return Unauthorized("Incorrect Password.");
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromForm] RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var uploadResult = await _cloudinarySetting.UploadImageAsync(registerUser.Image);

            var result = await _userService.AddUser(registerUser, uploadResult.Url.ToString(), Roles.CUSTOMER);
            if (result) return Ok(registerUser);
            return BadRequest("Có lỗi xảy ra");
        }

        [HttpGet("user/list/")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUsers(string? role, string? keyword)
        {
            if (role == "" || role == null) return Ok(await _userService.GetAllInfoUsers());

            if (keyword == "" || keyword == null) return Ok(await _userService.GetUsersByRole(role ?? Roles.EMPLOYEE));
            else return Ok(await _userService.GetUsersByKeyWordAndRole(role ?? Roles.EMPLOYEE, keyword));

        }
    }
}
