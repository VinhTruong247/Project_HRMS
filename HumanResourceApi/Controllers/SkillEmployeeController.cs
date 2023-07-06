using AutoMapper;
using HumanResourceApi.DTO.SkillEmployee;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillEmployeeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly SkillEmployeeRepo _skillEmployeeRepo;
        public Regex uniqueIdRegex = new Regex(@"^UN\d{6}");
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");
        public Regex skillIdRegex = new Regex(@"^SK\d{6}");

        public SkillEmployeeController(IMapper mapper, SkillEmployeeRepo skillEmployeeRepo)
        {
            _mapper = mapper;
            _skillEmployeeRepo = skillEmployeeRepo;
        }

        [HttpGet("SkillEmployees")]
        public IActionResult GetSkillEmployees()
        {
            try
            {
                var skillEmployeeList = _mapper.Map<List<SkillEmployeeDto>>(_skillEmployeeRepo.GetAll());
                return Ok(skillEmployeeList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/SkillEmployee/{uniqueId}")]
        public IActionResult GetSkillEmployeeById(string uniqueId)
        {
            try
            {
                if (!uniqueIdRegex.IsMatch(uniqueId))
                {
                    return BadRequest("Wrong uniqueId Format.");
                }
                var skillEmployee = _mapper.Map<SkillEmployeeDto>(_skillEmployeeRepo.GetAll().Where(se => se.UniqueId == uniqueId).FirstOrDefault());
                if (skillEmployee == null)
                {
                    return BadRequest("Unique ID = " + uniqueId + " doesn't seem to be found.");
                }
                return Ok(skillEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateSkillEmployee(SkillEmployeeDto skillEmployee)
        {
            try
            {
                if (skillEmployee == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!uniqueIdRegex.IsMatch(skillEmployee.UniqueId))
                {
                    return BadRequest("Wrong uniqueId Format.");
                }
                if (!employeeIdRegex.IsMatch(skillEmployee.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!employeeIdRegex.IsMatch(skillEmployee.SkillId))
                {
                    return BadRequest("Wrong skillId Format.");
                }
                if (_skillEmployeeRepo.GetAll().Any(se => se.UniqueId == skillEmployee.UniqueId))
                {
                    return BadRequest("Unique ID = " + skillEmployee.UniqueId + " existed");
                }
                var temp = _mapper.Map<SkillEmployee>(skillEmployee);
                _skillEmployeeRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/SkillEmployee/{uniqueId}")]
        public IActionResult UpdateSkillEmployee(string uniqueId, [FromBody] UpdateSkillEmployeeDto skillEmployee)
        {
            try
            {
                if (skillEmployee == null)
                {
                    return BadRequest();
                }
                if (!uniqueIdRegex.IsMatch(uniqueId))
                {
                    return BadRequest("Wrong uniqueId Format.");
                }
                if (!employeeIdRegex.IsMatch(skillEmployee.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!employeeIdRegex.IsMatch(skillEmployee.SkillId))
                {
                    return BadRequest("Wrong skillId Format.");
                }
                var validSkillEmployee = _skillEmployeeRepo.GetAll().Where(se => se.UniqueId == uniqueId).FirstOrDefault();
                if (validSkillEmployee == null)
                {
                    return BadRequest("Unique ID = " + uniqueId + " doesn't seem to be found.");

                }
                _mapper.Map(skillEmployee, validSkillEmployee);
                validSkillEmployee.UniqueId = uniqueId;

                _skillEmployeeRepo.Update(validSkillEmployee);
                return Ok(validSkillEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        //[HttpPost("delete")]
        //public IActionResult DeleteSkillEmployee([FromQuery] string id)
        //{
        //    var skillEmployee = _mapper.Map<SkillEmployeeDto>(_skillEmployeeRepo.GetAll().Where(se => se.UniqueId == id).FirstOrDefault());
        //    if (skillEmployee == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (skillEmployee.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validSE = _skillEmployeeRepo.GetAll().Where(se => se.UniqueId == id).FirstOrDefault();
        //    _mapper.Map(skillEmployee, validSE);
        //    validSE.Status = "Disable";
        //    _skillEmployeeRepo.Update(validSE);
        //    return Ok(validSE);
        //}
    }
}
