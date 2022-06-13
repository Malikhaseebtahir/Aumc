using System;
using Aumc.Core;
using AutoMapper;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PendingRequestsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPendingRequestRepository _repository;

        public PendingRequestsController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPendingRequestRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpPost("approveAttendance")]
        public async Task<IActionResult> ApprovePendingRequest(PendingAttendacneDetailsDto dto)
        {
            var attendance = await _repository.GetAttendance(dto.UserId, dto.GroupId);

            if (attendance == null)
                return NotFound("attendance not found");

            attendance.IsApproved = true;

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpGet("GetPendingRequestForGroupAdmin")]
        public async Task<IActionResult> GetPendingRequestForGroupAdmin()
        {
            var attendances = await _repository.GetAllPendingRequestForGroupAdmin(this.GetLoggedInUserId());
            
            return Ok(_mapper.Map<IEnumerable<Attendance>, IEnumerable<PendingAttendanceDto>>(attendances));
        }

        [HttpGet("GetPendingRequestForUser")]
        public async Task<IActionResult> GetAllPendingRequest()
        {
            var attendances = await _repository.GetAllPendingRequests(this.GetLoggedInUserId());

            return Ok(_mapper.Map<IEnumerable<Attendance>, IEnumerable<AttendanceReturnDto>>(attendances));
        }

        [HttpPost("rejectAttendance")]
        public async Task<IActionResult> DeletePendingRequest([FromBody] PendingAttendacneDetailsDto dto)
        {
            var attendance = await _repository.GetAttendance(dto.UserId, dto.GroupId);

            if (attendance == null)
                return NotFound("Not found");

            _repository.RemoveAttendance(attendance);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteRequestByUser(int groupId)
        {
            var attendance = await _repository.GetAttendance(this.GetLoggedInUserId(), groupId);

            if (attendance == null)
                return NotFound("not found");
            
            _repository.RemoveAttendance(attendance);
            await _unitOfWork.CompleteAsync();

            return Ok(groupId);
        }

        private int GetLoggedInUserId()
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}