using System.Collections.Generic;
using System.Threading.Tasks;
using Aumc.Core.Models;

namespace Aumc.Core
{
    public interface ICommentsRepository
    {
        Task AddAsync(Comment comment);
        void Delete(Comment comment);
        Task<Comment> GetCommentAsync(int commentId);
        Task<IEnumerable<Comment>> GetCommentsAsync(int groupId);
    }
}