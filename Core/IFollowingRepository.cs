using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aumc.Core
{
    public interface IFollowingRepository
    {
        Task AddAsync(Following following);
        void RemoveFollowee(Following following);
        Task<IEnumerable<Following>> GetAllFollowees(int userId);
        Task<IEnumerable<Following>> GetAllFollowers(int userId);
        Task<Following> GetUserFollowee(int followeeId, int userId);
        Task<bool> CheckingForFollowingAsync(int userId, int followeeId);
    }
}