using System;
using Aumc.Core;
using AutoMapper;
using Aumc.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GroupAttendanceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _repository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupAttendanceRepository _groupAttendanceRepository;

        public GroupAttendanceController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IGroupRepository repository,
            IGroupRepository groupRepository,
            IGroupAttendanceRepository groupAttendanceRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _groupRepository = groupRepository;
            _groupAttendanceRepository = groupAttendanceRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> GetUserAttendanceAsync()
        {
            var userId = this.GetUserId();
        
            return await _repository.GetGroupsByUser(userId);
        }

        [HttpGet("GroupUsers/{groupId}")]
        public async Task<IActionResult> GetGroupUsersAsync(int groupId)
        {
            var attendance = await _groupAttendanceRepository.GetAllGroupUsers(groupId);

            return Ok(_mapper.Map<IEnumerable<Attendance>, IEnumerable<GroupAttendanceReturnDto>>(attendance));
        }

        [HttpPost]
        public async Task<IActionResult> AttendAsync([FromBody] GroupAttendanceDto attendanceDto)
        {
            if (await _groupRepository.GetGroupAdmin(attendanceDto.GroupId) == this.GetUserId())
                return BadRequest("you cannot join your own group");           

            if (!await _groupAttendanceRepository.CheckForGroupAvailabilityAsync(attendanceDto.GroupId))
                return NotFound("Group not found");

            var userId = this.GetUserId();

            if (await _groupAttendanceRepository.CheckForUserAttendance(attendanceDto.GroupId, userId))
                return BadRequest("You already join the group");

            var attendance = new Attendance()
            {
                GroupId = attendanceDto.GroupId,
                UserId = userId
            };

            await _groupAttendanceRepository.AddAttendance(attendance);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("leaveGroup/{groupId}")]
        public async Task<IActionResult> DeleteAttendenceAsync(int groupId)
        {
            var userId = this.GetUserId();

            var userAttendance = await _groupAttendanceRepository.GetUserAttendance(groupId, userId);

            if (userAttendance == null)
                return NotFound("Not found");
            
            _groupAttendanceRepository.Remove(userAttendance);
            await _unitOfWork.CompleteAsync();

            return Ok(groupId);
        }

        private int GetUserId()
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}