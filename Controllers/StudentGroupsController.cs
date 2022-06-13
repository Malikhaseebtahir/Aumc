using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aumc.Controllers.ApiResources;
using Aumc.Core.Models;
using Aumc.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StudentGroupsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentGroupsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getAllStudentGroups")]
        public async Task<IEnumerable<object>> GetAllStudentGroupsAsync()
        {
            return await _context.StudentGroups.Where(g => g.IsApproved).Select(g => new 
            {
                Id = g.Id,
                Subject = g.Subject,
                Description = g.Description,
                UserId = g.UserId,
                Name = g.User.UserName,
                Title = g.User.TeacherOrStudent,
                LastActive = g.User.LastActive,
                Url = g.User.Photos.SingleOrDefault(p => p.IsMain == true).Url
            }).ToListAsync();
        }

        [HttpGet("getAllStudentGroupsForGroupCreator")]
        public async Task<IEnumerable<object>> GetAllStudentGroupsForCreatorAsync()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _context.StudentGroups.Where(g => g.IsApproved && g.UserId == userId).Select(g => new 
            {
                Id = g.Id,
                Subject = g.Subject,
                Description = g.Description,
                UserId = g.UserId,
                Name = g.User.UserName,
                Title = g.User.TeacherOrStudent,
                LastActive = g.User.LastActive,
                Url = g.User.Photos.SingleOrDefault(p => p.IsMain == true).Url
            }).ToListAsync();
        }

        [HttpGet("getAllStudentGroupsThatUserHasJoined")]
        public async Task<IActionResult> GetGroupsUserHasJoinAsync()
        {   
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var groups = await _context.StudentGroupAttendances
                .Include(a => a.Group)
                .Include(a => a.Group.User.Photos)
                .Where(a => a.UserId == userId && a.IsApproved)
                .ToListAsync(); 
            
            var result = _mapper.Map<IEnumerable<StudentGroupAttendance>, IEnumerable<AttendanceReturnDto>>(groups);
            return Ok(result);
        }

        [HttpGet("GetPendingRequestForUser")]
        public async Task<IActionResult> GetAllPendingRequest()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var attendances = await _context.StudentGroupAttendances
                .Include(a => a.Group)
                    .ThenInclude(g => g.User)
                        .ThenInclude(u => u.Photos)
                .Where(a => a.UserId == userId && !a.IsApproved)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentGroupAttendance>, IEnumerable<AttendanceReturnDto>>(attendances));
        }        

        [HttpGet("getStudentGroup/{groupId}")]
        public async Task<IActionResult> GetStudentGroupAsync(int groupId)
        {
            var group = await _context.StudentGroups
                .Include(g => g.User)
                .Include(g => g.StudentGroupPosts)
                .FirstOrDefaultAsync(g => g.Id == groupId && g.IsApproved);

            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentGroupAsync([FromBody]SaveStudentGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = _mapper.Map<SaveStudentGroupDto, StudentGroup>(groupDto);
            group.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.StudentGroups.AddAsync(group);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteRequestByUser(int groupId)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var attendance = await _context.StudentGroupAttendances
                .FirstOrDefaultAsync(a => a.UserId == userId && a.GroupId == groupId && !a.IsApproved);

            if (attendance == null)
                return NotFound("not found");
            
            _context.StudentGroupAttendances.Remove(attendance);
            await _context.SaveChangesAsync();            

            return Ok(groupId);
        }
    }
}