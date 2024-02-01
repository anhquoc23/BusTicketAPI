using BusApi.DTO;
using BusApi.Models;

namespace BusApi.Service
{
    public interface ICommentService
    {
        Task<bool> AddComment(Comment comment);
        Task<IEnumerable<CommentViewDTO>> GetCommentsByTripId(int id);
    }
}
