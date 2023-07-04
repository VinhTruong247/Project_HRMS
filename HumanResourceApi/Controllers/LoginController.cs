using AutoMapper;
using HumanResourceApi.DTO.Users;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserRepo _userRepo;
        private readonly IMapper _mapper;

        public LoginController(IConfiguration configuration, UserRepo userRepo, IMapper mapper)
        {
            _config = configuration;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        private string GenerateToken(User user)
        {
            var role = _userRepo.GetRole(user.RoleId);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role.RoleName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId)
                // Add more claims as needed
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Check if valid user -> generate token
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CheckLogin([FromBody] LoginDto loginInfo)
        {
            try
            {
                IActionResult response = Unauthorized();
                if (loginInfo == null)
                    return BadRequest(ModelState);

                var user_ = _userRepo.CheckLogin(loginInfo);

                if (user_ == null)
                {
                    return response;
                }
                
                var token = GenerateToken(user_);

                response = Ok(new { token = token });
                return response;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
