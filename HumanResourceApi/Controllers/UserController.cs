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
        private readonly RoleRepo _roleRepo;
        private readonly EmployeeRepo _empRepo;

        public UserController(IMapper mapper, UserRepo userRepo, RoleRepo roleRepo, EmployeeRepo empRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _empRepo = empRepo;
        }

        [HttpGet("get/users")]
        public IActionResult GetAll()
        {
            try
            {
                var userList = _userRepo.GetAllUsers();

                if (!ModelState.IsValid)
                {
                    // Handle invalid model state if needed
                    return BadRequest(ModelState);
                }

                var mappedUserList = userList.Select(l => _mapper.Map<ResponseUserDto>(l));

                // Return the list of users
                return Ok(mappedUserList);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/user")]
        public IActionResult GetUserById([FromQuery] string userId)
        {
            try
            {
                if (userId == null)
                    return BadRequest(ModelState);
                var tmpUser = _userRepo.GetById(userId);
                if (tmpUser == null)
                {
                    return NotFound();
                }
                var responseUser = _userRepo.GetAllUsers().Where(u => u.UserId == tmpUser.UserId).FirstOrDefault();
                var responseUserMapped = _mapper.Map<ResponseUserDto>(responseUser);
                return Ok(responseUserMapped);

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

                //Validation
                if (_userRepo.GetAll().Any(u => u.UserId == newUser.UserId))
                    return BadRequest("Duplicated UserId");
                if (!_roleRepo.GetAll().Any(u => u.RoleId == newUser.RoleId))
                    return BadRequest("Unavailable RoleId");
                if(!_empRepo.GetAll().Any(u => u.EmployeeId == newUser.EmployeeId))
                    return BadRequest("Unavailable EmployeeId");
                if (_userRepo.GetAll().Any(u => u.EmployeeId == newUser.EmployeeId))
                    return BadRequest("Duplicated EmployeeId");

                int count = _userRepo.GetAll().Count() + 1;
                var userId = "US" + count.ToString().PadLeft(6, '0');
                newUser.UserId = userId;
                _userRepo.Add(newUser);


                var responseUser = _userRepo.GetAllUsers().Where(u => u.UserId == newUser.UserId).FirstOrDefault();
                var responseUserMapped = _mapper.Map<ResponseUserDto>(responseUser);
                return Ok(responseUserMapped);
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
                var user = _userRepo.GetAll().Where(u => u.UserId == id).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }
                if (!_roleRepo.GetAll().Any(u => u.RoleId == updateUser.RoleId))
                    return BadRequest("Unavailable RoleId");

                _mapper.Map(updateUser, user);
                _userRepo.Update(user);
                var mappedUpdate = _mapper.Map<UserDto>(user);

                var responseUser = _userRepo.GetAllUsers().Where(u => u.UserId == id).FirstOrDefault();
                var responseUserMapped = _mapper.Map<ResponseUserDto>(responseUser);
                return Ok(responseUserMapped);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}