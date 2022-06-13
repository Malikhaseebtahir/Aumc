using System.Collections.Generic;
using System.Threading.Tasks;
using Aumc.Core.Models;

namespace Aumc.Core
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
    }        
}