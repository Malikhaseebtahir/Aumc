using Aumc.Core;
using System.Linq;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class PendingRequestRepository: IPendingRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public PendingRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Attendance> GetAttendance(int userId, int groupId)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(a => a.UserId == userId && a.GroupId == groupId && !a.IsApproved);
        }

        public async Task<IEnumerable<Attendance>> GetAllPendingRequestForGroupAdmin(int userId)
        {
            return await _context.Attendances
                .Include(a => a.Group)
                    .ThenInclude(g => g.User)
                .Include(a => a.User)
                    .ThenInclude(u => u.Photos)
                .Where(a => a.Group.UserId == userId && !a.IsApproved)
                .ToListAsync();

        }

        public async Task<IEnumerable<Attendance>> GetAllPendingRequests(int userId)
        {
            return await _context.Attendances
                .Include(a => a.Group)
                    .ThenInclude(g => g.User)
                        .ThenInclude(u => u.Photos)
                .Where(a => a.UserId == userId && !a.IsApproved)
                .ToListAsync();
        }

        public void RemoveAttendance(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}