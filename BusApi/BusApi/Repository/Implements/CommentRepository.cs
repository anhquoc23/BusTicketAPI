using BusApi.Data;
using BusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusApi.Repository.Implements
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDataContext _context;
        public CommentRepository(ApplicationDataContext context)
        {
            this._context = context;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            await this._context.Comments.AddAsync(comment);
            return await this._context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTripId(int id)
        {
            return await this._context.Comments.Include(u => u.Customer).Where(c => c.TripId == id).ToListAsync();
        }
    }
}
