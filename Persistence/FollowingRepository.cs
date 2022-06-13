using Aumc.Core;
using System.Linq;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class FollowingRepository: IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Following>> GetAllFollowees(int userId)
        {
            return await _context.Followings
                .Include(f => f.Follower)
                .Where(f => f.FollowerId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Following>> GetAllFollowers(int userId)
        {
            return await _context.Followings
                .Include(f => f.Followee)
                .Where(f => f.FolloweeId == userId)
                .ToListAsync();
        }

        public async Task<bool> CheckingForFollowingAsync(int userId, int followeeId)
        {
            return await _context.Followings
                .AnyAsync(f => f.FollowerId == userId && f.FolloweeId == followeeId);
        }

        public async Task<Following> GetUserFollowee(int followeeId, int userId)
        {
            return await _context.Followings
                .SingleOrDefaultAsync(f => f.FolloweeId == followeeId && f.FollowerId == userId);
        }

        public async Task AddAsync(Following following)
        {
            await _context.Followings.AddAsync(following);
        }

        public void RemoveFollowee(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}