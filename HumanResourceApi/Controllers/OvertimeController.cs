﻿using AutoMapper;
using HumanResourceApi.DTO.Overtime;
using HumanResourceApi.DTO.Report;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
                if ((overtime.Day - DateTime.Now).TotalDays < 2)
                {
                    return BadRequest("Overtime request must be 2 days old.");
                }
                int count = _overtimeRepo.GetAll().Count() + 1;
                var overtimeId = "OT" + count.ToString().PadLeft(6, '0');
                var temp = _mapper.Map<Overtime>(overtime);
                temp.Status = "Pending";
                temp.IsDeleted = false;
                temp.OvertimeId = overtimeId;

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

        [SwaggerOperation(Summary = "use date to find overtime")]
        [HttpGet("report/search/{date}")]
        public IActionResult FindEmployee(DateTime date)
        {
            try
            {
                var resultList = _mapper.Map<List<OvertimeDto>>(_overtimeRepo.GetAll().Where(rp => rp.Day.Month == date.Month));
                if (resultList == null)
                {
                    return BadRequest("No overtime(s) found");
                }
                return Ok(resultList.OrderByDescending(rp => rp.Day));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
