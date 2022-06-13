using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aumc.Controllers.ApiResources;
using Aumc.Core.Models;
using Aumc.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UniversityNewsController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UniversityNewsController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllStudentGroupNews()
        {
            var news = await _context.UniversityNews.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<UniversityNews>, IEnumerable<StudentGroupNewsLetterDto>>(news));
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateGroupNewsLetter(
            [FromBody]SaveUniversityNewsDto newsLetter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var news = _mapper.Map<SaveUniversityNewsDto, UniversityNews>(newsLetter);
            news.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.UniversityNews.AddAsync(news);
            await _context.SaveChangesAsync();

            var result = await _context.UniversityNews.FirstOrDefaultAsync(n => n.Id == news.Id);

            return Ok(_mapper.Map<UniversityNews, StudentGroupNewsLetterDto>(result));
        }

        [HttpPut()]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateStudentGroupNewsLetter(
            [FromBody] StudentGroupNewsLetterDto newsLetterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var news = await _context.UniversityNews
                .FirstOrDefaultAsync(n => n.Id == newsLetterDto.Id);

            _mapper.Map<StudentGroupNewsLetterDto, UniversityNews>(newsLetterDto, news);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<UniversityNews, StudentGroupNewsLetterDto>(news));
        }

        [HttpDelete("{newsId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteStudentGroupNews(int newsId)
        {
            var news = await _context.UniversityNews
                .FirstOrDefaultAsync(n => n.Id == newsId);
            
            if (news == null)
                return NotFound("not found");
            
            _context.UniversityNews.Remove(news);
            await _context.SaveChangesAsync();

            return Ok(newsId);
        }
    }
}