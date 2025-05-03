using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Instagram.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userDto)
        {
            var usuario = await _userService.GetUserByUsernameOrEmail(userDto.UserName, userDto.Email);

            if (usuario == null || usuario.Password != userDto.Password)
                return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });

            var token = GerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.UserName,
                    usuario.Email
                }
            });
        }

        private string GerarToken(User user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;

            await _userService.Update(existingUser);
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
