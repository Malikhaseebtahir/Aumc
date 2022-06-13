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
    public class CommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentsRepository _repository;
        public CommentsController(
            IMapper mapper,
            IUnitOfWork unitOfWork,            
            ICommentsRepository repository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpGet("allCommentsByGrouop/{groupId}")]
        public async Task<IActionResult> GetCommentsByGroup(int groupId)
        {
            var comments = await _repository.GetCommentsAsync(groupId);

            return Ok(_mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments));
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddCommentAsync(SaveCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _mapper.Map<SaveCommentDto, Comment>(commentDto);
            comment.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _repository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();

            var relatedResult = await _repository.GetCommentAsync(comment.Id);

            return Ok(_mapper.Map<Comment, CommentDto>(relatedResult));
        }

        [HttpDelete("deleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _repository.GetCommentAsync(commentId);

            if (comment == null)
                return NotFound("Comment not found");
            
            _repository.Delete(comment);
            await _unitOfWork.CompleteAsync();

            return Ok(commentId);
        }
    }
}