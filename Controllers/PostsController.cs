using System;
using Aumc.Core;
using AutoMapper;
using Aumc.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _repository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostsController(
            IMapper mapper,
            IGroupRepository repository,
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,          
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpGet("getPostCategories")]
        public async Task<IEnumerable<PostCategoryDto>> GetCategoriesAsync()
        {
            var PostCategories = await _postRepository.GetPostCategoriesAsync();
            return _mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryDto>>(PostCategories);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody]SavePostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var group = await _repository.GetGroupAsync(postDto.GroupId, includeRelated: false);
            
            var post = _mapper.Map<SavePostDto, Post>(postDto);
            post.IsApproved = true;
            group.LastUpdated = DateTime.Now;

            group.SendPostCreationNotification(await _userRepository.GetUsersForNotification(group.Id), post);

            await _postRepository.AddPostAsync(post);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Post, PostDto>(post));
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePostAsync(int postId, SavePostDto savePostDto)
        {
            var post = await _postRepository.GetPostAsync(postId);

            if (post == null)
                return NotFound("Post not found");
            
            var group = await _repository.GetGroupAsync(savePostDto.GroupId, includeRelated: false);
            
            group.SendPostUpdatedNotification(await _userRepository.GetUsersForNotification(group.Id), post);

            _mapper.Map<SavePostDto, Post>(savePostDto, post);

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<SavePostDto, Post>(savePostDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetPostAsync(id);

            if (post == null)
                return NotFound("Post not found");

            var group = await _repository.GetGroupAsync(post.GroupId, includeRelated: false);

            if (group == null)
                return NotFound("group not found");

            group.SendPostDeleteNotification(await _userRepository.GetUsersForNotification(group.Id), post);

            _postRepository.RemovePost(post);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}