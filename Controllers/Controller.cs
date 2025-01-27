using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Instagram.API.Data;
using Instagram.API.Model;

namespace Instagram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de dependência do DbContext
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return BadRequest("Usuário não encontrado");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Id == user.Id))
            {
                return BadRequest("Já existe esse usuário");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return NotFound("Usuário não encontrado");
            }

           
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário deletado com sucesso");
        }
    }
}
