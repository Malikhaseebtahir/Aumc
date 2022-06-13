using Aumc.Helpers;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Aumc.Core
{
    public interface IUserRepository
    {
        void Delete<T>(T obj);
        Task AddAsync<T>(T obj);
        Task<Photo> GetPhoto(int id);
        Task<Message> GetMessage(int id);
        Task<IEnumerable<Interest>> GetInterests();
        Task<Photo> GetUserProfilePhoto(int userId);
        Task<Like> GetUserLikee(int likeeId, int userId);
        Task<Like> GetLikes(int userId, int recipientId);
        Task<User> GetUserAsync(int id, bool isCurrentUser);
        Task<PagedList<User>> GetUsersAsync(UserParams userParams);
        Task<IEnumerable<User>> GetUsersByInterest(byte interestId);
        Task<IEnumerable<User>> GetUsersForNotification(int groupId);
        Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}