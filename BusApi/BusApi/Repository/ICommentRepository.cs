using BusApi.Models;

namespace BusApi.Repository
{
    public interface ICommentRepository
    {
        Task<bool> AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByTripId(int id);
    }
}
