using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Instagram.API.Data;
using Instagram.API.Model;
using Instagram.API.Services;

namespace Instagram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }
         
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return BadRequest("Usuário não encontrado"); 
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] User user)
        {
            var existingUser = await _userService.GetUserByUsernameOrEmail(user.UserName, user.Email);


            if (existingUser != null)
            {
                return BadRequest("Já existe esse usuário");
            }

          var createdUser = await _userService.CreateUser(user);
            

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var existingUser = await _userService.Update(user);

            if (existingUser == null)
            {
                return NotFound("Usuário não encontrado");
            }

           
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;

           

            return Ok("Usuário atualizado com sucesso");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound("Usuário não encontrado");
            }

           await _userService.DeleteUser(id);
            

            return Ok("Usuário deletado com sucesso");
        }
    }
}
