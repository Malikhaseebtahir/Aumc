using System;
using System.Threading.Tasks;
using Aumc.Core;
using Aumc.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Persistence
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username, bool includeLowerCaseUsername = true)
        {
            if (includeLowerCaseUsername)
            {
                return await _context.Users
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());            
            }

            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}