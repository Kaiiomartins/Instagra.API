using Microsoft.AspNetCore.Mvc;
using Instagram.API.Services;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        // USERS
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _userService.GetUserByUsernameOrEmail(username);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDto userDto)
        {
            await _userService.CreateUser(userDto);
            return StatusCode(StatusCodes.Status201Created, "Usuário criado com sucesso");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRequestDto user)
        {
            await _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await _userService.GetUserByUsernameOrEmail(username);
            if (user == null) return NotFound("Usuário não encontrado");

            await _userService.DeleteUser(username);
            return NoContent();
        }
    }
}
