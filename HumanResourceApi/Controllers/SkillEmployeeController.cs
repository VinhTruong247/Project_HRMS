using AutoMapper;
using HumanResourceApi.DTO.SkillEmployee;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillEmployeeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly SkillEmployeeRepo _skillEmployeeRepo;

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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/SkillEmployee/{uniqueId}")]
        public IActionResult GetSkillEmployeeById(string uniqueId)
        {
            var skillEmployee = _mapper.Map<SkillEmployeeDto>(_skillEmployeeRepo.GetAll().Where(se => se.UniqueId == uniqueId).FirstOrDefault());
            if (skillEmployee == null)
            {
                return BadRequest();
            }
            return Ok(skillEmployee);
        }

        [HttpPost("create")]
        public IActionResult CreateSkillEmployee(SkillEmployeeDto skillEmployee)
        {
            if (skillEmployee == null)
            {
                return BadRequest();
            }
            if (_skillEmployeeRepo.GetAll().Any(se => se.UniqueId == skillEmployee.UniqueId))
            {
                return BadRequest();
            }
            var temp = _mapper.Map<SkillEmployee>(skillEmployee);
            _skillEmployeeRepo.Add(temp);
            return Ok(temp);
        }

        [HttpPut("update/SkillEmployee/{uniqueId}")]
        public IActionResult UpdateSkillEmployee(string uniqueId, [FromBody] UpdateSkillEmployeeDto skillEmployee)
        {
            if (skillEmployee == null)
            {
                return BadRequest();
            }
            var validSkillEmployee = _skillEmployeeRepo.GetAll().Where(se => se.UniqueId == uniqueId).FirstOrDefault();
            if (validSkillEmployee == null)
            {
                return BadRequest();
            }
            _mapper.Map(skillEmployee, validSkillEmployee);
            validSkillEmployee.UniqueId = uniqueId;

            _skillEmployeeRepo.Update(validSkillEmployee);
            return Ok(validSkillEmployee);
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
