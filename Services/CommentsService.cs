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

        public async Task<CommentsResposeDto> CreateCommentsAsync(CommentsRequestDto comment)
        {
            var comments = await _repository.GetpostsAsync(comment.id, comment.DateComment);
            if (comments != null)
                throw new Exception("Comment not found");

            await _repository.CreaatePost(comment);

            var newComment = new CommentsResposeDto
            {
                id = comment.id,
                text = comment.TextComment,
                DateComment = comment.DateComment,
                DatewUpdate = DateTime.UtcNow
            };
            return newComment;
        }
        public async Task UpdateCommentsAsync(CommentsRequestDto comment)
        {
            var comments = await _repository.GetpostsAsync(comment.id, comment.DateComment);
            if (comments != null)
                throw new Exception("Comment not found");

            await _repository.PutCommentsAsync(comment);
        }
        public async Task DeleteCommentsAsync(CommentsRequestDto ComentsDelete) 
        {
            var comments = await _repository.GetpostsAsync(ComentsDelete.id, ComentsDelete.DateComment); 
            if (comments == null)
                throw new Exception("Comment not found");

            await _repository.DeleteCommentsAsync(ComentsDelete.id, ComentsDelete.DateComment); 
        }

       
    }
}
