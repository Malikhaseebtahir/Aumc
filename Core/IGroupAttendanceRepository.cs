using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aumc.Core
{
    public interface IGroupAttendanceRepository
    {
        void Remove(Attendance attendance);
        Task AddAttendance(Attendance attendance);
        Task<bool> CheckForGroupAvailabilityAsync(int groupId);
        Task<bool> CheckForUserAttendance(int groupId, int userId);
        Task<Attendance> GetUserAttendance(int groupId, int userId);
        Task<IEnumerable<Attendance>> GetAllGroupUsers(int groupId);      
    }
}