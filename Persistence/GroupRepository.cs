using Aumc.Core;
using System.Linq;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Aumc.Persistence
{
    public class GroupRepository: IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> GetGroupAdmin(int groupId)
        {
            return await _context.Groups
                .Where(g => g.Id == groupId)
                .Select(g => g.UserId)
                .SingleOrDefaultAsync();
        }

        public async Task<Group> GetGroupAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                await _context.Groups.FindAsync(id);

            return await _context.Groups
                .Include(g => g.User)
                    .ThenInclude(u => u.Photos)
                .Include(g => g.Posts)
                    .ThenInclude(p => p.PostCategory)
                .Include(g => g.GroupType)
                .SingleOrDefaultAsync(g => g.Id == id && !g.IsCancel);
        }

        public async Task<IEnumerable<object>> GetGroupsAsync()
        {
            return await _context.Groups.Where(g => !g.IsCancel).Select(g => new 
            {
                Id = g.Id,
                Subject = g.Subject,
                Description = g.Description,
                UserId = g.UserId,
                Name = g.User.UserName,
                Title = g.User.TeacherOrStudent,
                LastActive = g.User.LastActive,
                Url = g.User.Photos.SingleOrDefault(p => p.IsMain == true).Url
            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetGroupsByUser(int userId)
        {
            return await _context.Groups.Where(g => g.UserId == userId && !g.IsCancel)
                .Select(g => new  {
                    Id = g.Id,
                    Subject = g.Subject,
                    Description = g.Description,
                    UserId = g.UserId,
                    Name = g.User.UserName,
                    Title = g.User.TeacherOrStudent,
                    LastActive = g.User.LastActive,
                    Url = g.User.Photos.SingleOrDefault(p => p.IsMain == true).Url                 
                }).ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetGroupsUserHasJoinAsync(int userId)
        {
            return  await _context.Attendances
                .Include(a => a.Group)
                .Include(a => a.Group.User.Photos)
                .Where(a => a.UserId == userId && a.IsApproved && !a.Group.IsCancel)
                .ToListAsync();            
        }

        public async Task AddAsync(Group group)
        {
            await _context.AddAsync(group);
        }

        public void Remove(Group group)
        {
            _context.Groups.Remove(group);
        }
    }
}