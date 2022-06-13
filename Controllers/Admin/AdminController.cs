using AutoMapper;
using System.Linq;
using Aumc.Core.Models;
using CloudinaryDotNet;
using Aumc.Persistence;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet.Actions;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Aumc.Controllers.ApiResources;
using Microsoft.AspNetCore.Authorization;

namespace Aumc.Controllers.Admin
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public AdminController(
            IMapper mapper,
            ApplicationDbContext context,
            UserManager<User> userManager,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users orderby user.UserName
                                    select new {
                                        Id = user.Id,
                                        UserName = user.UserName,
                                        Title = user.TeacherOrStudent,
                                        LastActive = user.LastActive,
                                        Roles = (from userRole in user.UserRoles
                                                    join role in _context.Roles
                                                    on userRole.RoleId
                                                    equals role.Id
                                                    select role.Name).ToList()
                                    }).ToListAsync();

            return Ok(userList);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found");
            
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(userId);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public async Task<IActionResult> GetPhotosForModeratorsAsync()
        {
            var photos = await _context.Photos
                .Include(u => u.User)
                .IgnoreQueryFilters()
                .Where(p => p.IsApproved == false)
                .Select(u => new 
                {
                    Id = u.Id,
                    UserName = u.User.UserName,
                    Url = u.Url,
                    IsApproved = u.IsApproved
                }).ToListAsync();

            return Ok(photos);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("postForModeration")]
        public async Task<IActionResult> GetPostForModeratorsAsync()
        {
            var post = await _context.Posts
                .IgnoreQueryFilters()
                .Where(p => p.IsApproved == false)
                .Select(p => new 
                {
                    Id = p.Id,
                    Title = p.Title,
                    GroupId = p.GroupId,
                    Description = p.Description,
                    IsApproved = p.IsApproved
                }).ToListAsync();

            return Ok(post);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("groupsForModeration")]
        public async Task<IActionResult> GetGroupForModeratorsAsync()
        {
            var group = await _context.Groups
                .Include(u => u.UserId)
                .IgnoreQueryFilters()
                .Where(p => p.IsApproved == false)
                .Select(u => new 
                {
                    Id = u.Id,
                    UserName = u.User.UserName,
                    IsApproved = u.IsApproved
                }).ToListAsync();

            return Ok(group);
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("studentGroupsForModeration")]
        public async Task<IActionResult> GetStudentGroupForModeratorsAsync()
        {
            var group = await _context.StudentGroups
                .Include(u => u.UserId)
                .Where(p => !p.IsApproved)
                .Select(u => new 
                {
                    Id = u.Id,
                    UserName = u.User.UserName,
                    IsApproved = u.IsApproved
                }).ToListAsync();

            return Ok(group);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("interestsForModeration")]
        public async Task<IActionResult> GetInterestsForModeratorsAsync()
        {
            var interest = await _context.Interests
                .IgnoreQueryFilters()
                .Where(i => i.IsApproved == false)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<Interest>, IEnumerable<InterestDto>>(interest));
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approvePhoto/{photoId}")]
        public async Task<IActionResult> ApprovePhoto(int photoId)
        {
            var photo = await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            photo.IsApproved = true;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("approveGroup/{groupId}")]
        public async Task<IActionResult> ApproveGroup(int groupId)
        {
            var group = await _context.Groups
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Id == groupId);

            group.IsApproved = true;

            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("approveStudentGroup/{groupId}")]
        public async Task<IActionResult> ApproveStudentGroup(int groupId)
        {
            var group = await _context.StudentGroups
                .FirstOrDefaultAsync(g => g.Id == groupId && !g.IsApproved);

            group.IsApproved = true;

            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("approveInterest/{interestId}")]
        public async Task<IActionResult> ApproveInterest(byte interestId)
        {
            var interest = await _context.Interests
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(i => i.Id == interestId);

            if (interest == null)
                return NotFound("Interest not found");
            
            interest.IsApproved = true;

            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("approvePost/{postId}")]        
        public async Task<IActionResult> ApprovePost(int postId)
        {
            var post = await _context.Posts
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == postId);

            post.IsApproved = true;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("rejectPhoto/{photoId}")]
        public async Task<IActionResult> RejectPhoto(int photoId)
        {
            var photo = await _context.Photos
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == photoId);

            if (photo.IsMain)
                return BadRequest("You cannot reject the main photo");

            if (photo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _context.Photos.Remove(photo);
                }
            }

            if (photo.PublicId == null)
            {
                _context.Photos.Remove(photo);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("rejectGroup/{groupId}")]
        public async Task<IActionResult> RejectGroup(int groupId)
        {
            var group = await _context.Groups
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(g => g.Id == groupId);
            
            if (group == null)
                return NotFound("Group not found");
            
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("rejectStudentGroup/{groupId}")]
        public async Task<IActionResult> RejectStudentGroup(int groupId)
        {
            var group = await _context.StudentGroups
                .FirstOrDefaultAsync(g => g.Id == groupId && !g.IsApproved);
            
            if (group == null)
                return NotFound("Group not found");
            
            _context.StudentGroups.Remove(group);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("rejectPost/{postId}")]
        public async Task<IActionResult> RejectPost(int postId)
        {
            var post = await _context.Posts
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == postId);
            
            if (post == null)   
                return NotFound("Post not found");
            
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("reportsForModeration")]
        public async Task<IActionResult> GetReportsForModerationAsync()
        {
            var reports = await _context.UserReports
                .IgnoreQueryFilters()
                .OrderBy(ru => ru.Id)
                .Include(ur => ur.Reportee)
                    .ThenInclude(u => u.Photos)
                .Include(ur => ur.Reporter)
                    .ThenInclude(u => u.Photos)
                .Where(ru => ru.IsApproved == false)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<UserReport>, IEnumerable<UserReportDto>>(reports);
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("approveReport")]
        public async Task<IActionResult> ApproveReport(ApproveReportDto approveReportDto)
        {
            var report = await _context.UserReports
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(ru => ru.Id == approveReportDto.ReportId);

            if (report == null)
                return NotFound("Report not found");
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == report.ReporteeId);

            if (user == null)
                return NotFound("User with this report number not found");
            
            user.IsDisabled = true;
            report.IsApproved = true;

            var blockUser = new BlockUser
            {
                UserId = user.Id,
                TeacherOrStudent = user.TeacherOrStudent,
                KnownAs = user.KnownAs,
                Message = approveReportDto.Message,
                ClassName = (user.ClassName != null) ? user.ClassName : "bachelor"
            };

            await _context.BlockUsers.AddAsync(blockUser);
            await _context.SaveChangesAsync();

            return Ok(approveReportDto);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("rejectReport/{id}")]
        public async Task<IActionResult> RejectReport(int id)
        {
            var report = await _context.UserReports
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(ur => ur.Id == id);
            
            if (report == null)
                return NotFound("Report not found");
            
            _context.UserReports.Remove(report);
            await _context.SaveChangesAsync();

            return Ok(id);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("disabledUser")]
        public async Task<IActionResult> GetDisabledUser()
        {
            var users =  await _context.Users
                .IgnoreQueryFilters()
                .Where(u => u.IsDisabled == true)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("enableUser/{userId}")]
        public async Task<IActionResult> EnableUser(int userId)
        {
            var user = await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user == null)
                return NotFound("User not found");
            
            user.IsDisabled = false;
            await _context.SaveChangesAsync();

            return Ok(userId);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("getAllTheBlockUserReports")]
        public async Task<IActionResult> GetAppTheBlockUserReports()
        {
            var blockReports =  await _context.BlockUsers
                .Include(bu => bu.User)
                    .ThenInclude(u => u.Photos)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<BlockUser>, IEnumerable<BlockUserDto>>(blockReports));
        }
    }
}