using AutoMapper;
using HumanResourceApi.DTO.Overtime;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly OvertimeRepo _overtimeRepo;
        public Regex overtimeIdRegex = new Regex(@"^OT\d{6}");
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public OvertimeController(IMapper mapper, OvertimeRepo overtimeRepo)
        {
            _mapper = mapper;
            _overtimeRepo = overtimeRepo;
        }

        [HttpGet("get/overtime")]
        public IActionResult GetOvertimeList()
        {
            try
            {
                var overtimeList = _mapper.Map<List<OvertimeDto>>(_overtimeRepo.GetAll());
                if (overtimeList == null)
                {
                    return BadRequest("No Overtime Log(s) found.");
                }
                return Ok(overtimeList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/overtime/{employeeId}")]
        public IActionResult GetOvertimeOfEmployee(string employeeId)
        {
            try
            {
                var overtimeList = _mapper.Map<List<OvertimeDto>>(_overtimeRepo.GetAll().Where(e => e.EmployeeId == employeeId));
                if (overtimeList == null)
                {
                    return BadRequest("No Overtime Log(s) found.");
                }
                return Ok(overtimeList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/overtime/{employeeId}/{overtimeId}")]
        public IActionResult GetDetailOvertime(string employeeId, string overtimeId)
        {
            try
            {
                var overtime = _mapper.Map<OvertimeDto>(_overtimeRepo.GetAll().Where(e => e.EmployeeId == employeeId && e.OvertimeId == overtimeId).FirstOrDefault());
                if (overtime == null)
                {
                    return BadRequest("Overtime ID = " + overtimeId + " doesn't seem to be found");
                }
                return Ok(overtime);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateOvertime([FromBody] CreateOvertimeDto overtime)
        {
            try
            {
                if (overtime == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!employeeIdRegex.IsMatch(overtime.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!overtimeIdRegex.IsMatch(overtime.OvertimeId))
                {
                    return BadRequest("Wrong overtimeId Format.");
                }
                if (_overtimeRepo.GetAll().Any(e => e.EmployeeId == overtime.EmployeeId && e.OvertimeId == overtime.OvertimeId))
                {
                    return BadRequest("Overtime ID = " + overtime.OvertimeId + " existed");
                }
                if ((overtime.Day - DateTime.Now).TotalDays < 2)
                {
                    return BadRequest("Overtime request must be 2 days old.");
                }
                var temp = _mapper.Map<Overtime>(overtime);
                temp.Status = "Pending";
                temp.IsDeleted = false;

                _overtimeRepo.Add(temp);
                return Ok(_mapper.Map<OvertimeDto>(temp));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/overtime/{employeeId}/{overtimeId}")]
        public IActionResult UpdateOvertime(string employeeId, string overtimeId, [FromBody] UpdateOvertimeDto overtime)
        {
            try
            {
                if (overtime == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!overtimeIdRegex.IsMatch(overtimeId))
                {
                    return BadRequest("Wrong overtimeId Format.");
                }
                var validOvertime = _overtimeRepo.GetAll().Where(e => e.EmployeeId == employeeId && e.OvertimeId == overtimeId).FirstOrDefault();
                if (validOvertime == null)
                {
                    return BadRequest("Overtime ID = " + overtimeId + " doesn't seem to be found");
                }
                _mapper.Map(overtime, validOvertime);
                validOvertime.EmployeeId = employeeId;
                validOvertime.OvertimeId = overtimeId;

                _overtimeRepo.Update(validOvertime);
                return Ok(_mapper.Map<OvertimeDto>(validOvertime));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
