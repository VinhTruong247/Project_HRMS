using AutoMapper;
using HumanResourceApi.Interfaces;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using HumanResourceApi.DTO.Users;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepository;
        private readonly IMapper _mapper;
        private readonly UserRepo _userRepo;

        public UserController(IUser userRepository, IMapper mapper, UserRepo userRepo)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userRepo = userRepo;
        }

        
        [HttpGet("Get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var userList = _mapper.Map<List<UserDto>>(_userRepo.GetAll());

                if (!ModelState.IsValid)
                {
                    // Handle invalid model state if needed
                    return BadRequest(ModelState);
                }

                // Return the list of users
                return Ok(userList);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Check-login")]
        public IActionResult CheckLogin([FromBody] LoginDto loginInfo)
        {
            try
            {
                if (loginInfo == null)
                    return BadRequest(ModelState);

                var account = _userRepo.CheckLogin(loginInfo.Username, loginInfo.Password);

                if (account == null)
                {
                    return BadRequest("Wrong username or password");
                }

                var accountDto = _mapper.Map<UserDto>(account);
                return Ok(accountDto);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Get-user")]
        public IActionResult getUserById([FromQuery] int userId)
        {
            try
            {
                if (userId == null)
                    return BadRequest(ModelState);

                var userMap = _mapper.Map<UserDto>(_userRepo.GetById(userId));

                if (userMap == null)
                {
                    return NotFound();
                }

                return Ok(userMap);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("Add-user")]

        public IActionResult addUser([FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User is null");
                }
                var tmpUser = _userRepo.GetAll()
                    .Where(u => u.Username.Trim().ToUpper() == user.Username.Trim().ToUpper())
                    .FirstOrDefault();
                if (tmpUser != null)
                {
                    return BadRequest("Username already exists");
                }

                var newUser = _mapper.Map<User>(user);
                newUser.Role = _userRepo.GetRole(user.RoleId);

                //check userId duplicate or unavailable roleId
                if (_userRepo.GetAll().Any(u => u.UserId == newUser.UserId))
                    return BadRequest("Duplicated Id");
                if (!_userRepo.GetAll().Any(u => u.RoleId == newUser.RoleId))
                    return BadRequest("Unavailable RoleId");


                _userRepo.Add(newUser);

                // Configure JSON serializer options
                var serializerOptions = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                // Serialize the newUser object to JSON with the configured options
                var newUserJson = JsonSerializer.Serialize(newUser, serializerOptions);

                return Ok(newUserJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
