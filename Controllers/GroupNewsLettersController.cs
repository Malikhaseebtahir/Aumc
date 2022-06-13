using System.Collections.Generic;
using System.Linq;
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
    public class GroupNewsLettersController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public GroupNewsLettersController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getAllGroupNews/{groupId}")]
        public async Task<IActionResult> GetAllStudentGroupNews(int groupId)
        {
            var news = await _context.GroupNewsLetters
                .Where(n => n.GroupId == groupId)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<GroupNewsLetter>, IEnumerable<StudentGroupNewsLetterDto>>(news));

        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupNewsLetter(
            [FromBody]SaveStudentGroupNewsLetterDto newsLetter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var news = _mapper.Map<SaveStudentGroupNewsLetterDto, GroupNewsLetter>(newsLetter);
            await _context.GroupNewsLetters.AddAsync(news);
            await _context.SaveChangesAsync();

            var result = await _context.GroupNewsLetters.FirstOrDefaultAsync(n => n.Id == news.Id);

            return Ok(_mapper.Map<GroupNewsLetter, StudentGroupNewsLetterDto>(result));
        }

        [HttpPut("updateGroupNews")]
        public async Task<IActionResult> UpdateStudentGroupNewsLetter(
            [FromBody] StudentGroupNewsLetterDto newsLetterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var news = await _context.GroupNewsLetters
                .FirstOrDefaultAsync(n => n.Id == newsLetterDto.Id);

            _mapper.Map<StudentGroupNewsLetterDto, GroupNewsLetter>(newsLetterDto, news);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<GroupNewsLetter, StudentGroupNewsLetterDto>(news));
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> DeleteStudentGroupNews(int newsId)
        {
            var news = await _context.GroupNewsLetters
                .FirstOrDefaultAsync(n => n.Id == newsId);
            
            if (news == null)
                return NotFound("not found");
            
            _context.GroupNewsLetters.Remove(news);
            await _context.SaveChangesAsync();

            return Ok(newsId);
        }
    }
}