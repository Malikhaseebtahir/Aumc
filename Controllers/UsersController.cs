using System;
using Aumc.Core;
using AutoMapper;
using Aumc.Helpers;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("/api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;
        public UsersController(
            IMapper mapper, 
            IUserRepository repository, 
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UserParams userParams)
        {
            var currentUserId = this.GetCurrentlyLogedInUserId();

            var userFromRepo = await _repository.GetUserAsync(currentUserId, true);

            userParams.UserId = currentUserId;

            // if (string.IsNullOrWhiteSpace(userParams.Gender))
            // {
            //     userParams.Gender = userFromRepo.Gender == "male" ? "female": "male";               
            // }

            var users = await _repository.GetUsersAsync(userParams);
            var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<UserListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPage);

            return Ok(usersDto);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var currentUser = this.IsAuthorized(id);
            //  = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value) == id;
            var user = await _repository.GetUserAsync(id, currentUser);
            
            var userDto = _mapper.Map<User, UserDetailsDto>(user);

            return Ok(userDto);
        }

        [HttpGet("getUsersByInterest/{interestId}")]
        public async Task<IActionResult> GetUsersByInterest(byte interestId)
        {
            var users = await _repository.GetUsersByInterest(interestId);

            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserListDto>>(users));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (!this.IsAuthorized(id))
                return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var user = await this._repository.GetUserAsync(id, true);
            if (user == null)
                return NotFound();

            var loggedInUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (loggedInUserId != id)
                return Unauthorized();

            var userDto = _mapper.Map<UserUpdateDto, User>(userUpdateDto, user);
            await this._unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<User, UserDetailsDto>(userDto));
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUserAsync(int id, int recipientId)
        {
            if (!this.IsAuthorized(id))
                return Unauthorized();

            var like = await this._repository.GetLikes(id, recipientId);

            if (like != null)
                return BadRequest("You already like this user");
            
            if (await this._repository.GetUserAsync(recipientId, false) == null)
                return NotFound("User not found");

            like = new Like
            {
                LikeeId = recipientId,
                LikerId = id
            };

            like.Notify(await _repository.GetUserAsync(recipientId, false), await _repository.GetUserAsync(id, false));

            await this._repository.AddAsync<Like>(like);
            await this._unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("{likeeId}")]
        public async Task<IActionResult> RemoveLike(int likeeId)
        {
            var like = await _repository.GetUserLikee(likeeId, this.GetCurrentlyLogedInUserId());

            if (like == null)
                return NotFound("User not found");
            
            _repository.Delete<Like>(like);
            await _unitOfWork.CompleteAsync();

            return Ok(likeeId);
        }

        private bool IsAuthorized(int userId)
        {
            return userId == this.GetCurrentlyLogedInUserId();
        }

        private int GetCurrentlyLogedInUserId()
        {
            return Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}