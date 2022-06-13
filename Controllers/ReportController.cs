using System;
using AutoMapper;
using Aumc.Persistence;
using Aumc.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aumc.Controllers.ApiResources;

namespace Aumc.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ReportController(
            IMapper mapper,            
            ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportUserAsync([FromBody]SaveUserReportDto userReportDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userReport = _mapper.Map<SaveUserReportDto, UserReport>(userReportDto);

            userReport.ReporterId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _context.UserReports.AddAsync(userReport);
            await _context.SaveChangesAsync();

            return Ok(userReport);
        }
    }
}