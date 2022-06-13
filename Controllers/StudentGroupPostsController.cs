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
    public class StudentGroupPostsController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentGroupPostsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getAllApprovedStudentGroupPosts/{groupId}")]
        public async Task<IActionResult> GetAllApproveStudentGroupPosts(int groupId)
        {
            var posts = await _context.StudentGroupPosts
                .Include(p => p.User)
                .Where(p => p.GroupId == groupId && p.IsApproved).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentGroupPost>, IEnumerable<StudentGroupPostDto>>(posts));
        }

        [HttpGet("getAllStudentGroupPostsForApproval/{groupId}")]
        public async Task<IActionResult> GetStudentGroupPosts(int groupId)
        {
            var posts = await _context.StudentGroupPosts
                .Include(p => p.User)
                .Where(p => p.GroupId == groupId && !p.IsApproved).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentGroupPost>, IEnumerable<StudentGroupPostDto>>(posts));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentGroupPost([FromBody]SaveStudentGroupPostDto groupPostDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var post = _mapper.Map<SaveStudentGroupPostDto, StudentGroupPost>(groupPostDto);
            post.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.StudentGroupPosts.AddAsync(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("approveStudentGroupPost/{postId}")]
        public async Task<IActionResult> ApproveStudentGroupPost(int postId)
        {
            var post = await _context.StudentGroupPosts.FirstOrDefaultAsync(p => p.Id == postId && !p.IsApproved);

            if (post == null)
                return NotFound("not found");

            post.IsApproved = true;
            await _context.SaveChangesAsync();

            return Ok();            
        }

        [HttpDelete("removeStudentGroupPost/{postId}")]
        public async Task<IActionResult> RemoveStudentGroupPost(int postId)
        {
            var post = await _context.StudentGroupPosts.FirstOrDefaultAsync(p => p.Id == postId && !p.IsApproved);

            if (post == null)
                return NotFound("not found");

            _context.StudentGroupPosts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(postId);
        }
    }
}