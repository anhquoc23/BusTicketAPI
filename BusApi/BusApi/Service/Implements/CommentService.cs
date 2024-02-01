using BusApi.DTO;
using BusApi.Models;
using BusApi.Repository;

namespace BusApi.Service.Implements
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            return await this._commentRepository.AddComment(comment);
        }

        public async Task<IEnumerable<CommentViewDTO>> GetCommentsByTripId(int id)
        {
            var comments = await this._commentRepository.GetCommentsByTripId(id);
            var result = comments.Select(x => new CommentViewDTO
            {
                FullName = $"{x.Customer.LastName} {x.Customer.FirstName}",
                Content = x.Content,
                Avatar = x.Customer.Avatar,
                CreatedDate = x.CreatedDate,
            });
            return result;
        }
    }
}
