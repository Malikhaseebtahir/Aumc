using AutoMapper;
using System.Linq;
using Aumc.Core.Models;
using Aumc.Persistence;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Aumc.Controllers.ApiResources;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Aumc.Controllers.Admin
{
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class InterestsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public InterestsController(
            IMapper mapper,            
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<InterestDto>> GetInterestsAsync()
        {
            var interests = await _context.Interests.OrderBy(i => i.Name).ToListAsync();
            return _mapper.Map<IEnumerable<Interest>, IEnumerable<InterestDto>>(interests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInterest(InterestDto interestDto)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var interest = _mapper.Map<InterestDto, Interest>(interestDto);

            if (user.UserName == "Admin") 
                interest.IsApproved = true;
            
            await _context.Interests.AddAsync(interest);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<Interest, InterestDto>(interest));
        }
    }
}