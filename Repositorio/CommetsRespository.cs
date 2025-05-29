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
        public async Task<CommentsResposeDto> GetpostsAsync(int id, DateTime dateComment)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id && c.DateComment == dateComment);

            if (comment == null)
                throw new KeyNotFoundException("Comment not found");

            var response = new CommentsResposeDto
            {
                id = comment.Id,
                text = comment.Commets,
                DateComment = comment.DateComment,
                DatewUpdate = comment.DateUpdated
            };

            return response;
        }
        public async Task CreaatePost(CommentsRequestDto commentsRequestDto)
        {
            var comment = new Comments
            {
                Commets = commentsRequestDto.TextComment,
                DateComment = commentsRequestDto.DateComment,
                DateUpdated = DateTime.Now,
                IsDeleted = false,
           
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }
        public async Task PutCommentsAsync(CommentsRequestDto commentsRequestDto)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == commentsRequestDto.id);
            if (comment == null)
                throw new KeyNotFoundException("Comment not found");

            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCommentsAsync(int id, DateTime Date)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                throw new KeyNotFoundException("Comment not found");
     
            comment.IsDeleted = true;
            comment.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

        }
    }
}

