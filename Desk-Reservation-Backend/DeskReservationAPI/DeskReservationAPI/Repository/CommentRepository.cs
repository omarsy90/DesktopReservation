using DeskReservationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DeskReservationAPI.Repository
{
    public interface ICommentRepository
    {
        Task<Comment> Add(Comment comment);
        Task<Comment> GetCommentByID(int commentID);
        Task<IEnumerable<Comment>> GetComments();
        Task<IEnumerable<Comment>> GetCommentsByDesk(int deskID);
        Task<IEnumerable<Comment>> GetCommentsByUser(string userID);
    }

    public class CommentRepository : ICommentRepository
    {
        private ApplicationDBContext _dbContext;
        public CommentRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _dbContext.Comments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByDesk(int deskID)
        {
            return await _dbContext.Comments.Where(co => co.DeskID == deskID).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUser(string userID)
        {
            return await _dbContext.Comments.Where(co => co.UserID == userID).AsNoTracking().ToListAsync();
        }

        public async Task<Comment> GetCommentByID(int commentID)
        {
            return await _dbContext.Comments.AsNoTracking().FirstOrDefaultAsync(co => co.CommentID == commentID);
        }

        public async Task<Comment> Add(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

    }
}
