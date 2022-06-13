using Aumc.Core;
using System.Linq;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class CommentsRepository: ICommentsRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int groupId)
        {
            return await _context.Comments
                .Where(c => c.GroupId == groupId)
                .Include(c => c.User)
                    .ThenInclude(u => u.Photos)
                .ToListAsync();
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Group)
                .FirstOrDefaultAsync(c => c.Id == commentId);
        }
    }
}