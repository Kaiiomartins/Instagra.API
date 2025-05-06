using Microsoft.AspNetCore.Mvc;
using Instagram.API.Models;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return BadRequest("Usuário não encontrado");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDto userDto)
        {
            var user = await _userService.CreateUser(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var existingUser = await _userService.GetUserById(user.Id);
            if (existingUser == null)
                return NotFound("Usuário não encontrado");

            await _userService.UpdateUser(existingUser);
            return Ok("Usuário atualizado com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound("Usuário não encontrado");

            await _userService.DeleteUser(id);
            return Ok("Usuário deletado com sucesso");
        }
    }
}
