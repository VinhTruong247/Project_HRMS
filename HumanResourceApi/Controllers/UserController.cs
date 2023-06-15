using AutoMapper;
using HumanResourceApi.DTO;
using HumanResourceApi.Interfaces;
using HumanResourceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUser userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("Get-all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var categories = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                // Handle invalid model state if needed
                return BadRequest(ModelState);
            }

            // Return the list of users
            return Ok(categories);
        }
        [HttpPost("GetUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CheckLogin([FromQuery] int userId)
        {
            if (userId == null)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<UserDto>(_userRepository.GetUserById(userId));
            
            if(userMap == null)
            {
                return NotFound();
            }

            return Ok(userMap);
        }

        [HttpPost("Login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CheckLogin([FromBody] LoginDto loginInfo)
        {
            if (loginInfo == null)
                return BadRequest(ModelState);

            bool exist = _userRepository.CheckLogin(loginInfo.Username, loginInfo.Password);
            if(!exist)
            {
                return Ok("Wrong username or Password");
            }
            
            return Ok("Valid");
        }
    }
}
