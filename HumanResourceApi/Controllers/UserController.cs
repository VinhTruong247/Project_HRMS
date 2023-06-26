using AutoMapper;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using HumanResourceApi.DTO.Users;
using Microsoft.AspNetCore.Authorization;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IMapper _mapper;
        private readonly UserRepo _userRepo;

        public UserController(IMapper mapper, UserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [Authorize]
        [HttpGet("get/users")]
        public IActionResult GetAll()
        {
            try
            {
                var userList = _mapper.Map<List<UserDto>>(_userRepo.GetAll().Where(u => u.Status == "1"));

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

        [Authorize]
        [HttpPost("get/user")]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            try
            {
                if (userId == null)
                    return BadRequest(ModelState);
                var tmpUser = _userRepo.GetById(userId);
                if (tmpUser.Status != "1")
                {
                    return NotFound();
                }
                var userMap = _mapper.Map<UserDto>(tmpUser);

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


        [HttpPost("add")]

        public IActionResult AddUser([FromBody] UserDto user)
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

                //check userId duplicate or unavailable roleId
                if (_userRepo.GetAll().Any(u => u.UserId == newUser.UserId))
                    return BadRequest("Duplicated Id");
                if (!_userRepo.GetAll().Any(u => u.RoleId == newUser.RoleId))
                    return BadRequest("Unavailable RoleId");


                _userRepo.Add(newUser);

               

                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/update")]
        public IActionResult UpdateUser(string id, [FromBody] UpdateUserDto updateUser)
        {
            try
            {
                if (updateUser == null)
                {
                    return BadRequest();
                }
                var tmpUser = _userRepo.GetAll()
                    .Where(u => u.Username.Trim().ToUpper() == updateUser.Username.Trim().ToUpper())
                    .FirstOrDefault();
                if (tmpUser != null)
                {
                    return BadRequest("Username already exists");
                }
                var user = _userRepo.GetAll().Where(u => u.UserId == id && u.Status == "1").FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }
                if (!_userRepo.GetAll().Any(u => u.RoleId == updateUser.RoleId))
                    return BadRequest("Unavailable RoleId");

                _mapper.Map(updateUser, user);
                user.UserId = id;
                _userRepo.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("remove")]
        public IActionResult DeleteUser(string id)
        {
            var user = _userRepo.GetAll().Where(u => u.UserId == id && u.Status == "1").FirstOrDefault();
            if (user == null)
            {
                return NotFound(id);
            }
            user.Status = "0";
            _userRepo.Update(user);
            return Ok(user);
        }
    }
}