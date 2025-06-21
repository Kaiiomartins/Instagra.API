using Instagram.API.Data;
using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Instagram.API.Repositorio
{
    public class CommetsRespository : ICommetsRepository
    {
        AppDbContext _context;
        public CommetsRespository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Comments> GetpostsAsync(int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }
        public async Task CreateComments(Comments commentsRequestDto)
        {
            await _context.Comments.AddAsync(commentsRequestDto);
            await _context.SaveChangesAsync();
        }
        public async Task PutCommentsAsync(Comments commentsRequestDto)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentsRequestDto.Id);
     
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCommentsAsync(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
      
            comment.IsDeleted = true;
            comment.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

        }
    }
}

