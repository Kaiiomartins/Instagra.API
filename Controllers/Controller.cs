using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Instagram.API.Data;
using Instagram.API.Model;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;

namespace Instagram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private List<User> users = new List<User>();

        [HttpGet]
        public async Task<IActionResult> getUser(int id)
        {

            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null) {
                return BadRequest("Usuario não encontrado");
            }
            return Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> createUser([FromBody] User user)  {

            if (users.Any(u => u.Id == user.Id)) {
                return BadRequest("Já existe esse funcionario");
            }
            users.Add(user);

            return CreatedAtAction(nameof(getUser), new { id = user.Id }, user);
}

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]User user) {

            var usuario = users.FirstOrDefault(u => u.Id == user.Id);

            if (usuario == null) {
                return NotFound("Usuario não encontrado"); ;
            }
            usuario.UserName = user.UserName;
            usuario.email = user.email;
            usuario.DataNascimento = user.DataNascimento;
            usuario.Passaword = user.Passaword;
            
            return Ok("Usuário atualizado com sucesso");

        }

        [HttpDelete]
        public async Task<IActionResult> Delete( int id) {


            var usuario = users.FirstOrDefault(u => u.Id == id);

            if (usuario == null) {
                return NotFound("Usuario não encontrado");
            }
            users.Remove(usuario);

            return Ok("Usuario deletado com sucesso");
        }


            
        }

    }

