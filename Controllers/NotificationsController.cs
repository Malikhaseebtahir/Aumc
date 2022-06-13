using System;
using AutoMapper;
using System.Linq;
using Aumc.Core.Models;
using Aumc.Persistence;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class NotificationsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public NotificationsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewNotificationsAsync()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var notification = await _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)   
                .Select(un => un.Notification)
                .Include(n => n.Group.User)
                .Include(n => n.Follower)
                .Include(n => n.Liker)
                .ToListAsync();
        
            return Ok(_mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notification));
        }
    }
}