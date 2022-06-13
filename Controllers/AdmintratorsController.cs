using System.Collections.Generic;
using System.Threading.Tasks;
using Aumc.Core.Models;
using Aumc.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aumc.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class AdmintratorsController: Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdmintratorsController(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUserRoles()
        {
            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
                new Role{Name = "Moderator"},
                new Role{Name = "VIP"},
            };

            foreach (var role in roles)
            {
                _roleManager.CreateAsync(role).Wait();
            }

            return Ok();
        }
    }
}