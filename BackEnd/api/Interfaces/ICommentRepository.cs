using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int Id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment> DeleteAsync(int Id);
    }
}