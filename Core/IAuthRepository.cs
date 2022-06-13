using System.Threading.Tasks;
using Aumc.Core.Models;

namespace Aumc.Core
{
    public interface IAuthRepository
    {
        Task<User> GetUser(string username, bool includeLowerCaseUsername = true);
    }
}