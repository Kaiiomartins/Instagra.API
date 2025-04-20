using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Instagram.API.Data;
using Instagram.API.Models;
using PostsWebApi.Servicos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CvaminhoController : ControllerBase
    {
        private readonly ServicesPosts _servicesPosts;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CvaminhoController(ServicesPosts servicesPosts, AppDbContext context, IConfiguration configuration)
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

        // POSTS
        // POSTS
        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _servicesPosts.GetPostById(id);
            if (post is null)
                return NotFound(new { mensagem = "Post não encontrado." });

            return Ok(post);
            
        }

        [HttpPost("post/texto")]
        public async Task<IActionResult> CreatePostText([FromBody] Posts posts)
        {
            var post = await _servicesPosts.GetPostById(posts.Id);
            if (post is null)
                return BadRequest("Já existe esse post");

            var created = await _servicesPosts.CreatePosts(posts);
            return Ok(created);
        }


        [HttpPost("post/imagem")]
        public async Task<IActionResult> CreatePostImagem(
            [FromForm] int userId,
            [FromForm] string description,
            [FromForm] string postType,
            [FromForm] DateTime postDate,
            [FromForm] IFormFile imagem)
        {
            var post = new Posts
            {
                UserId = userId,
                Description = description,
                PostType = postType,
                PostDate = postDate
            };

            var criado = await _servicesPosts.CreatePostComImagemAsync(post, imagem);
            return Ok(criado);
        }

        [HttpGet("post/imagem/{postId}")]
        public async Task<IActionResult> VisualizarImagem(int postId)
        {
            var caminhoRelativo = await _servicesPosts.GetCaminhoImagemAsync(postId);

            if (string.IsNullOrEmpty(caminhoRelativo))
                return NotFound("Imagem não encontrada.");

            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativo.TrimStart('/'));

            if (!System.IO.File.Exists(caminhoFisico))
                return NotFound("Arquivo não existe no disco.");

            var extensao = Path.GetExtension(caminhoFisico).ToLowerInvariant();
            var contentType = extensao switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            var bytes = await System.IO.File.ReadAllBytesAsync(caminhoFisico);
            return File(bytes, contentType);
        }

        [HttpPut("post")]
        public async Task<IActionResult> PutPost([FromBody] Posts posts)
        {
            var postExistente = await _servicesPosts.GetPostById(posts.Id);
            if (postExistente is  null)
                return NotFound(new { mensagem = "Post não encontrado." });

            var postAtualizado = await _servicesPosts.UpdatePostAsync(posts);
            return Ok(postAtualizado);
        }

        [HttpDelete("post/{id}")]
        public async Task<IActionResult> DeletePosts(int id)
        {
            var postExistente = await _servicesPosts.GetPostById(id);
            if (postExistente is null)
                return NotFound(new { mensagem = "Post não encontrado." });

            var postDeletado = await _servicesPosts.DeletesPostAsync(id);
            return Ok(postDeletado);
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
