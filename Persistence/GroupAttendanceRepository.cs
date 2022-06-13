using Aumc.Core;
using System.Linq;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class GroupAttendanceRepository: IGroupAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupAttendanceRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Attendance> GetUserAttendance(int groupId, int userId)
        {
            return await _context.Attendances
                .SingleOrDefaultAsync(a => a.UserId == userId && a.GroupId == groupId);            
        }

        public async Task<IEnumerable<Attendance>> GetAllGroupUsers(int groupId)
        {
            return await _context.Attendances
                .Include(a => a.User)
                    .ThenInclude(u => u.Photos)
                .OrderBy(a => a.DateJoin)
                .Where(a => a.GroupId == groupId).ToListAsync();            
        }

        public async Task<bool> CheckForGroupAvailabilityAsync(int groupId)
        {
            return await _context.Groups.AnyAsync(g => g.Id == groupId);
        }

        public async Task<bool> CheckForUserAttendance(int groupId, int userId)
        {
            return await _context.Attendances.AnyAsync(a => a.GroupId == groupId && a.UserId == userId);
        }

        public async Task AddAttendance(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}