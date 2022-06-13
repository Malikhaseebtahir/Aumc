using System;
using Aumc.Core;
using AutoMapper;
using System.Text;
using Aumc.Core.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Aumc.Controllers.ApiResources;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Aumc.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            IMapper mapper, 
            IConfiguration config,
            UserManager<User> userManager,
            IAuthRepository authRepository,          
            SignInManager<User> signInManager)
        {
            _config = config;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            if (user == null) 
            {
                user = await _authRepository.GetUser(userForLoginDto.UserName, includeLowerCaseUsername: true);
                
                if (user == null)
                    return Unauthorized();

                var result1 = await _signInManager
                    .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

                if (!result1.Succeeded)
                    return Unauthorized();

                if (user.IsDisabled)
                    return BadRequest("your account is currently disabled please contact admin office");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.UserName.ToUpper());

                var userToReturn = _mapper.Map<UserListDto>(appUser);

                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }        
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UseRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            userToCreate.Address.Country = "Pakistan";

            var user = await _authRepository.GetUser(userForRegisterDto.UserName, includeLowerCaseUsername: false);

            if (user != null)
                return BadRequest("User with this identity already exists");

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<UserDetailsDto>(userToCreate);

            if (result.Succeeded)
            {
                if (userToCreate.TeacherOrStudent == "teacher")
                    await _userManager.AddToRoleAsync(userToCreate, "Moderator");
                if (userToCreate.TeacherOrStudent == "student")
                    await _userManager.AddToRoleAsync(userToCreate, "Member");

                return CreatedAtRoute("GetUser", 
                    new { controller = "Users", id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);
        }
    }
}