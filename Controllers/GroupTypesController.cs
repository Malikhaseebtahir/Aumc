using AutoMapper;
using Aumc.Core.Models;
using Aumc.Persistence;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aumc.Controllers.ApiResources;
using Microsoft.EntityFrameworkCore;

namespace Aumc.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class GroupTypesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public GroupTypesController(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

    public async Task<IEnumerable<GroupTypeDto>> GetGroupTypesAsync()
    {
        var groupTypes = await _context.GroupTypes.ToListAsync();

        return _mapper.Map<IEnumerable<GroupType>, IEnumerable<GroupTypeDto>>(groupTypes);
    }
}
}