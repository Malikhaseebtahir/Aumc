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
    public class FollowingsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IFollowingRepository _repository;

        public FollowingsController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IFollowingRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet("GetAllFollowees")]
        public async Task<IActionResult> GetAllFolloweesAsync()
        {
            var userId = this.GetUserLoggedInId();

            var followess = await _repository.GetAllFollowees(userId);
            
            return Ok(_mapper.Map<IEnumerable<Following>, IEnumerable<FollowingDto>>(followess));
        }

        [HttpGet("GetAllFollowers")]
        public async Task<IActionResult> GetAllFollowersAsync()
        {
            var userId = this.GetUserLoggedInId();

            var followers = await _repository.GetAllFollowers(userId);

            return Ok(_mapper.Map<IEnumerable<Following>, IEnumerable<FollowingDto>>(followers));
        }

        [HttpPost("{followeeId}")]
        public async Task<IActionResult> FollowAsync(int followeeId)
        {
            var userId = this.GetUserLoggedInId();

            if (await _repository.CheckingForFollowingAsync(userId, followeeId))
                return BadRequest("You are already following this user");

            var following = new Following
            {
                FolloweeId = followeeId,
                FollowerId = userId
            };

            following.Notify(await _userRepository.GetUserAsync(followeeId, false), await _userRepository.GetUserAsync(userId, false));

            await _repository.AddAsync(following);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("{followeeId}")]
        public async Task<IActionResult> UnfollowUser(int followeeId)
        {
            var followee = await _repository.GetUserFollowee(followeeId, this.GetUserLoggedInId());

            if (followee == null)
                return NotFound("user not found");

            _repository.RemoveFollowee(followee);
            await _unitOfWork.CompleteAsync();

            return Ok(followeeId);
        }

        private int GetUserLoggedInId()
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}