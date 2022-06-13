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
    public class StudentGroupAttendancesController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentGroupAttendancesController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroupAttendanceForApproval()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var attendances = await _context.StudentGroupAttendances
                .Include(a => a.Group)
                    .ThenInclude(g => g.User)
                .Include(a => a.User)
                .Where(a => !a.IsApproved && a.Group.UserId == userId)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentGroupAttendance>, IEnumerable<StudentGroupPendingAttendanceDto>>(attendances));
        }

        [HttpGet("StudentGroupUsers/{groupId}")]
        public async Task<IActionResult> GetGroupUsersAsync(int groupId)
        {
            var attendance = await _context.StudentGroupAttendances
                .Include(a => a.User)
                    .ThenInclude(u => u.Photos)
                .OrderBy(a => a.DateJoin)
                .Where(a => a.GroupId == groupId).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentGroupAttendance>, IEnumerable<GroupAttendanceReturnDto>>(attendance));
        }

        [HttpPost]
        public async Task<IActionResult> AttendStudentGroupAsync(SaveStudentGroupAttendanceDto dto)
        {
            var groupAdminUserId = await _context.Groups
                .Where(g => g.Id == dto.GroupId)
                .Select(g => g.UserId)
                .SingleOrDefaultAsync();

            if (groupAdminUserId == this.GetUserId())
                return BadRequest("you cannot join your own group");           

            if (!await _context.StudentGroups.AnyAsync(g => g.Id == dto.GroupId))
                return NotFound("Group not found");

            var userId = this.GetUserId();

            if (await _context.StudentGroupAttendances.AnyAsync(a => a.GroupId == dto.GroupId && a.UserId == userId))
                return BadRequest("You already join the group");

            var attendance = new StudentGroupAttendance()
            {
                GroupId = dto.GroupId,
                UserId = userId
            };

            await _context.StudentGroupAttendances.AddAsync(attendance);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("approveStudentGroupAttendance")]        
        public async Task<IActionResult> ApproveAttendance(PendingAttendacneDetailsDto dto)
        {
            var attendance = await _context.StudentGroupAttendances
                .FirstOrDefaultAsync(a => a.UserId == dto.UserId && a.GroupId == dto.GroupId && !a.IsApproved);
        
            if (attendance == null)
                return NotFound("attendance not found");

            attendance.IsApproved = true;

            await _context.SaveChangesAsync();

            return Ok();        
        }

        [HttpPost("rejectStudentGroupAttendance")]
        public async Task<IActionResult> DeletePendingRequest([FromBody] PendingAttendacneDetailsDto dto)
        {
            var attendance = await _context.StudentGroupAttendances
                .FirstOrDefaultAsync(a => a.UserId == dto.UserId && a.GroupId == dto.GroupId && !a.IsApproved);
            if (attendance == null)
                return NotFound("Not found");

            _context.StudentGroupAttendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("leaveGroup/{groupId}")]
        public async Task<IActionResult> DeleteAttendenceAsync(int groupId)
        {
            var userId = this.GetUserId();

            var userAttendance = await _context.StudentGroupAttendances
                .SingleOrDefaultAsync(a => a.UserId == userId && a.GroupId == groupId);

            if (userAttendance == null)
                return NotFound("Not found");

            _context.StudentGroupAttendances.Remove(userAttendance);
            await _context.SaveChangesAsync();            

            return Ok(groupId);
        }

        private int GetUserId()
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}