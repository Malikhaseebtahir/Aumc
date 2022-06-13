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
    public class StudentGroupNewsLettersController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentGroupNewsLettersController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getAllStudentGroupNews/{groupId}")]
        public async Task<IActionResult> GetAllStudentGroupNews(int groupId)
        {
            var news = await _context.StudentGroupNewsLetters
                .Where(n => n.GroupId == groupId)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentGroupNewsLetter>, IEnumerable<StudentGroupNewsLetterDto>>(news));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentGroupNewsLetter(
            [FromBody]SaveStudentGroupNewsLetterDto newsLetter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var news = _mapper.Map<SaveStudentGroupNewsLetterDto, StudentGroupNewsLetter>(newsLetter);
            await _context.StudentGroupNewsLetters.AddAsync(news);
            await _context.SaveChangesAsync();

            var result = await _context.StudentGroupNewsLetters.FirstOrDefaultAsync(n => n.Id == news.Id);

            return Ok(_mapper.Map<StudentGroupNewsLetter, StudentGroupNewsLetterDto>(result));
        }

        [HttpPut("updateStudentGroupNews")]
        public async Task<IActionResult> UpdateStudentGroupNewsLetter(
            [FromBody] StudentGroupNewsLetterDto newsLetterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var news = await _context.StudentGroupNewsLetters
                .FirstOrDefaultAsync(n => n.Id == newsLetterDto.Id);

            _mapper.Map<StudentGroupNewsLetterDto, StudentGroupNewsLetter>(newsLetterDto, news);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<StudentGroupNewsLetter, StudentGroupNewsLetterDto>(news));
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> DeleteStudentGroupNews(int newsId)
        {
            var news = await _context.StudentGroupNewsLetters
                .FirstOrDefaultAsync(n => n.Id == newsId);
            
            if (news == null)
                return NotFound("not found");
            
            _context.StudentGroupNewsLetters.Remove(news);
            await _context.SaveChangesAsync();

            return Ok(newsId);
        }
    }
}