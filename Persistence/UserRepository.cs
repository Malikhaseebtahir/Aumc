using Aumc.Core;
using System.Linq;
using Aumc.Helpers;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Interest>> GetInterests()
        {
            return await _context.Interests.ToListAsync();
        }

        public async Task<PagedList<User>> GetUsersAsync(UserParams userParams)
        {
            var users = _context.Users.Include(u => u.Photos).OrderByDescending(u => u.LastActive).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);
            // users = users.Where(u => u.Gender == userParams.Gender);
            users = users.Where(u => u.TeacherOrStudent != "teacher");
            
            if (userParams.Likers)
            {
                var userLikers = await GetUserLikesAsync(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikers.Any(liker => liker.LikerId == u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikesAsync(userParams.UserId, userParams.Likers);
                users = users.Where(u => userLikees.Any(likee => likee.LikeeId == u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99) 
            {
                users = users.Where(u => u.GetAge >= userParams.MinAge && u.GetAge <= userParams.MaxAge);
            }

            if (!string.IsNullOrWhiteSpace(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IEnumerable<User>> GetUsersByInterest(byte interestId)
        {
            var users = await _context.Users
                .Include(u => u.Photos)
                .Where(u => u.InterestId == interestId)
                .ToListAsync();
            
            return users;
        }

        public async Task<User> GetUserAsync(int id, bool isCurrentUser)
        {
            var query = _context.Users
                .Include(u => u.Photos)
                .Include(u => u.Interest)
                .AsQueryable();

            if (isCurrentUser)
                query = query.IgnoreQueryFilters();

            var user = await query.FirstOrDefaultAsync(u => u.Id == id);
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersForNotification(int groupId)
        {
            return await _context.Attendances
                .Where(a => a.GroupId == groupId)
                .Select(a => a.User)
                .ToListAsync();            
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Photo> GetUserProfilePhoto(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain == true);
        }

        public async Task<Like> GetLikes(int userId, int recipientId)
        {
            return await this._context.Likes
                .FirstOrDefaultAsync(u => u.LikerId == userId && u.LikeeId == recipientId);
        }

        public async Task AddAsync<T>(T obj)
        {
            await this._context.AddAsync(obj);
        }

        public void Delete<T>(T obj)
        {
            _context.Remove(obj);
        }

        private async Task<IEnumerable<Like>> GetUserLikesAsync(int id, bool likers)
        {
            var users = await this._context.Users
                        .Include(u => u.Likee)
                        .Include(u => u.Liker)
                        .FirstOrDefaultAsync(u => u.Id == id);

            if (likers)
            {
                return users.Likee.Where(u => u.LikeeId == id);
            }
            else
            {
                return users.Liker.Where(u => u.LikerId == id);
            }

        }

        public async Task<Message> GetMessage(int id)
        {
            return await this._context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();
            
            switch(messageParams.MessageContainer)
            {
                case "Inbox":
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId && u.RecipientDeleted == false);
                    break;
                case "Outbox":
                    messages = messages.Where(u => u.SenderId == messageParams.UserId && u.SenderDeleted == false);
                    break;
                default:
                    messages = messages.Where(u => u.RecipientId == messageParams.UserId 
                        && u.RecipientDeleted == false && u.IsRead == false);
                    break;
            }

            messages = messages.OrderByDescending(m => m.MessageSent);
            
            return await PagedList<Message>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);

        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .Where(m => (m.RecipientId == userId && m.RecipientDeleted == false && m.SenderId == recipientId) 
                    || (m.RecipientId == recipientId && m.SenderId == userId && m.SenderDeleted == false))
                .OrderByDescending(m => m.MessageSent)
                .ToListAsync();
            
            return messages;
        }

        public async Task<Like> GetUserLikee(int likeeId, int userId)
        {
            return await _context.Likes
                .Where(l => l.LikeeId == likeeId && l.LikerId == userId)
                .SingleOrDefaultAsync();
        }
    }
}