using System.Collections.Generic;
using System.Threading.Tasks;
using Aumc.Core.Models;

namespace Aumc.Core
{
    public interface IPendingRequestRepository
    {
        void RemoveAttendance(Attendance attendance);
        Task<Attendance> GetAttendance(int userId, int groupId);
        Task<IEnumerable<Attendance>> GetAllPendingRequests(int userId);
        Task<IEnumerable<Attendance>> GetAllPendingRequestForGroupAdmin(int userId);
    }
}