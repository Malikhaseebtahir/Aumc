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
    [Route("/api/[controller]")]
    [ApiController]
    public class GroupsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _repository;
        private readonly IUserRepository _userRepository;

        public GroupsController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IGroupRepository repository,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<object>> GetGroupsAsync()
        {
            return await _repository.GetGroupsAsync();
        }

        [HttpGet("GroupsByUser")]
        public async Task<IEnumerable<object>> GetGroupsByUser()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var groups = await _repository.GetGroupsByUser(userId);

            return groups;
        }

        [HttpGet("GetJoinGroups/{userId}")]
        public async Task<IActionResult> GetGroupsUserHasJoinAsync(int userId)
        {   
            var groups = await _repository.GetGroupsUserHasJoinAsync(userId);
            
            var result = _mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceReturnDto>>(groups);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupAsync(int id)
        {
            var group = await _repository.GetGroupAsync(id);

            if (group == null)
                return NotFound("Group not found");

            var result = _mapper.Map<Group, GroupDto>(group);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync([FromBody] SaveGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = _mapper.Map<SaveGroupDto, Group>(groupDto);
            group.LastUpdated = DateTime.Now;
            group.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _repository.AddAsync(group);
            await _unitOfWork.CompleteAsync();

            group = await _repository.GetGroupAsync(group.Id);

            var result = _mapper.Map<Group, GroupDto>(group);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupAsync(int id, [FromBody] SaveGroupDto groupDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = await _repository.GetGroupAsync(id, includeRelated: false);

            if (group == null)
                return NotFound("Group not found");
            
            group.SendNotification(await _userRepository.GetUsersForNotification(id), groupDto.Room);
            
            group.LastUpdated = DateTime.Now;
            _mapper.Map<SaveGroupDto, Group>(groupDto, group);

            await _unitOfWork.CompleteAsync();

            group = await _repository.GetGroupAsync(group.Id);


            var result = _mapper.Map<Group, GroupDto>(group);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DisableGroupAsync(int id)
        {
            var group = await _repository.GetGroupAsync(id);
            
            if (group  == null)
                return NotFound("Group not found");

            group.Cancel(await _userRepository.GetUsersForNotification(id));

            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}