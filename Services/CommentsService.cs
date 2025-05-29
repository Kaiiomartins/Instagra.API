using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Instagram.API.Services
{

    
public class CommentsService : ICommentsService
    {
        private readonly CommetsRespository _repository;

        public CommentsService(CommetsRespository repository)
        {
            _repository = repository;
        }

        public async Task<CommentsResposeDto> GetCommentsAsync(CommentsRequestDto comment)
        {
            var commment = await _repository.GetpostsAsync(comment.id, comment.DateComment);
            if (commment == null)
                throw new Exception("Comment not found"); 

            var comments = new CommentsResposeDto
            {
                id = comment.id,
                text = comment.TextComment,
                DateComment = comment.DateComment,
                DatewUpdate = DateTime.UtcNow
            };

            return comments;
        }

        public Task<CommentsResposeDto> CreateCommentsAsync(CommentsRequestDto comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommentsAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentsAsync(CommentsRequestDto comment)
        {
            throw new NotImplementedException();
        }
    }
}
