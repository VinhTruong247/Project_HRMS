using AutoMapper;
using HumanResourceApi.DTO.Leave;
using HumanResourceApi.DTO.Report;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LeaveRepo _leaveRepo;
        public Regex leaveIdRegex = new Regex(@"^LE\d{6}");
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public LeaveController(IMapper mapper, LeaveRepo leaveRepo)
        {
            _mapper = mapper;
            _leaveRepo = leaveRepo;
        }

        [HttpGet("leaves")]
        public IActionResult GetLeave()
        {
            try
            {
                var leaveList = _mapper.Map<List<LeaveDto>>(_leaveRepo.GetAll());
                return Ok(leaveList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("get/leave/{leaveId}")]
        public IActionResult GetLeaveId(string leaveId)
        {
            try
            {
                if (!leaveIdRegex.IsMatch(leaveId))
                {
                    return BadRequest("Wrong leaveId Format.");
                }
                var leave = _mapper.Map<LeaveDto>(_leaveRepo.GetAll().Where(l => l.LeaveId == leaveId).FirstOrDefault());
                if (leave == null)
                {
                    return BadRequest("Leave ID = " + leaveId + " doesn't seem to be found.");
                }
                return Ok(leave);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateLeave([FromBody] LeaveDto leave)
        {
            try
            {
                if (leave == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!leaveIdRegex.IsMatch(leave.LeaveId))
                {
                    return BadRequest("Wrong leaveId Format.");
                }
                if (!employeeIdRegex.IsMatch(leave.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                bool validLeave = _leaveRepo.GetAll().Any(l => l.LeaveId == leave.LeaveId);
                if (validLeave)
                {
                    return BadRequest("Leave ID = " + leave.LeaveId + " existed");
                }
                var temp = _mapper.Map<Leave>(leave);
                _leaveRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update/leave/{leaveId}")]
        public IActionResult UpdateLeave(string leaveId, [FromBody] UpdateLeaveDto leave)
        {
            try
            {
                if (leave == null)
                {
                    return BadRequest();
                }
                if (!leaveIdRegex.IsMatch(leaveId))
                {
                    return BadRequest("Wrong leaveId Format.");
                }
                if (!employeeIdRegex.IsMatch(leave.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var validLeave = _leaveRepo.GetAll().Where(l => l.LeaveId == leaveId).FirstOrDefault();
                if (validLeave == null)
                {
                    return BadRequest("Leave ID = " + leaveId + " doesn't seem to be found.");
                }
                _mapper.Map(leave, validLeave);
                validLeave.LeaveId = leaveId;

                _leaveRepo.Update(validLeave);
                return Ok(validLeave);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }
    }
}
