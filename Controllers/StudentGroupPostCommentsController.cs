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
    public class StudentGroupPostCommentsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentGroupPostCommentsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("allPostComments/{postId}")]
        public async Task<IActionResult> GetCommentsByGroup(int postId)
        {
            var comments = await _context.StudentGroupPostComments
                .Where(c => c.PostId== postId)
                .Include(c => c.User)
                    .ThenInclude(u => u.Photos)
                .ToListAsync();
                
            return Ok(_mapper.Map<IEnumerable<StudentGroupPostComment>, IEnumerable<PostCommentDto>>(comments));
        }

        [HttpPost("addPostComment")]
        public async Task<IActionResult> AddCommentAsync([FromBody]SavePostCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postComment = _mapper.Map<SavePostCommentDto, StudentGroupPostComment>(commentDto);
            postComment.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.StudentGroupPostComments.AddAsync(postComment);
            await _context.SaveChangesAsync();

            var relatedResult = await _context.StudentGroupPostComments
                .Include(pc => pc.User)
                .Include(pc => pc.Post)
                .FirstOrDefaultAsync(pc => pc.Id == postComment.Id);

           return Ok(_mapper.Map<StudentGroupPostComment, PostCommentDto>(relatedResult));
        }

        [HttpDelete("deletePostComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.StudentGroupPostComments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                return NotFound("Comment not found");
            
            _context.StudentGroupPostComments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(commentId);
        }     
    }
}