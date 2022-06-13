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
    public class PostCommentsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public PostCommentsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("allPostComments/{postId}")]
        public async Task<IActionResult> GetCommentsByGroup(int postId)
        {
            var comments = await _context.PostComments
                .Where(c => c.PostId== postId)
                .Include(c => c.User)
                    .ThenInclude(u => u.Photos)
                .ToListAsync();
                
            return Ok(_mapper.Map<IEnumerable<PostComment>, IEnumerable<PostCommentDto>>(comments));
        }

        [HttpPost("addPostComment")]
        public async Task<IActionResult> AddCommentAsync([FromBody]SavePostCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postComment = _mapper.Map<SavePostCommentDto, PostComment>(commentDto);
            postComment.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.PostComments.AddAsync(postComment);
            await _context.SaveChangesAsync();

            var relatedResult = await _context.PostComments
                .Include(pc => pc.User)
                .Include(pc => pc.Post)
                .FirstOrDefaultAsync(pc => pc.Id == postComment.Id);

            return Ok(_mapper.Map<PostComment, PostCommentDto>(relatedResult));
        }

        [HttpDelete("deletePostComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.PostComments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                return NotFound("Comment not found");
            
            _context.PostComments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(commentId);
        }
    }
}