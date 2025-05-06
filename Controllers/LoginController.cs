using Instagram.API.Models;
using Instagram.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Instagram.API.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userService, IConfiguration configuration)
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
        public IActionResult Index()
        {
            var model = new { Message = "Bem-vindo à página inicial!" };
            return View(model);
        }

    }
}
