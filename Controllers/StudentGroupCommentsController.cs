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
    public class StudentGroupCommentsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentGroupCommentsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("allStudentGroupCommentsByGroup/{groupId}")]
        public async Task<IActionResult> GetCommentsByGroup(int groupId)
        {
            var comments = await _context.StudentGroupComments
                .Where(c => c.GroupId == groupId)
                .Include(c => c.User)
                    .ThenInclude(u => u.Photos)
                .ToListAsync();
                
            return Ok(_mapper.Map<IEnumerable<StudentGroupComment>, IEnumerable<CommentDto>>(comments));
        }

        [HttpPost("AddStudentGroupComment")]
        public async Task<IActionResult> AddCommentAsync(SaveCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _mapper.Map<SaveCommentDto, StudentGroupComment>(commentDto);
            comment.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.StudentGroupComments.AddAsync(comment);
            await _context.SaveChangesAsync();

            var relatedResult = await _context.StudentGroupComments
                .Include(c => c.User)
                .Include(c => c.Group)
                .FirstOrDefaultAsync(c => c.Id == comment.Id);

            return Ok(_mapper.Map<StudentGroupComment, CommentDto>(relatedResult));
        }

        [HttpDelete("deleteStudentGroupComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var comment = await _context.StudentGroupComments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                return NotFound("Comment not found");
            
            _context.StudentGroupComments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(commentId);
        }
    }
}