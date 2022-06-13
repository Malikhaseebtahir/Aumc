using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aumc.Core
{
    public interface IGroupRepository
    {
        void Remove(Group group);
        Task AddAsync(Group group);
        Task<int> GetGroupAdmin(int groupId);
        Task<IEnumerable<object>> GetGroupsAsync();
        Task<IEnumerable<object>> GetGroupsByUser(int userId);
        Task<Group> GetGroupAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Attendance>> GetGroupsUserHasJoinAsync(int userId);
    }
}