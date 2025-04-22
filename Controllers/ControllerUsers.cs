using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Instagram.API.Data;
using Instagram.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Instagram.API.Services;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaminhoController : ControllerBase
    {
        private readonly PostsService _servicesPosts;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CaminhoController(PostsService servicesPosts, AppDbContext context, IConfiguration configuration)
        {
            _servicesPosts = servicesPosts;
            _context = context;
            _configuration = configuration;
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User usuarioLogin)
        {
            var usuario = await _servicesPosts.GetUserByUserName(usuarioLogin.UserName);

            if (usuario == null || usuario.Password != usuarioLogin.Password)
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
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return BadRequest("Usuário não encontrado");
            return Ok(user);
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] User users)
        {
            if (await _context.Users.AnyAsync(u => u.Id == users.Id))
                return BadRequest("Já existe esse usuário");

            await _context.Users.AddAsync(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = users.Id }, users);
        }

        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
                return NotFound("Usuário não encontrado");

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Usuário atualizado com sucesso");
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound("Usuário não encontrado");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário deletado com sucesso");
        }
    }
}
