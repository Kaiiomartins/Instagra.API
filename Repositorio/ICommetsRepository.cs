using Instagram.API.Models;
using Instagram.API.Models.Dtos;
namespace Instagram.API.Repositorio
{
    public interface ICommetsRepository
    {

        public Task <Comments> GetpostsAsync(int id);

        public Task CreateComments(Comments comments);

        public Task PutCommentsAsync(Comments commentsRequestDto);

        public Task DeleteCommentsAsync(int id);
    }
}
