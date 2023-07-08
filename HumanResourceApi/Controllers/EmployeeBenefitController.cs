using AutoMapper;
using HumanResourceApi.DTO.EmployeeBenefit;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeBenefitRepo _employeeBenefitRepo;
        Regex employeeBenefitIdRegex = new Regex(@"^EB\d{6}");
        Regex allowanceIdRegex = new Regex(@"^AL\d{6}");
        Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public EmployeeBenefitController(IMapper mapper, EmployeeBenefitRepo employeeBenefitRepo)
        {
            _mapper = mapper;
            _employeeBenefitRepo = employeeBenefitRepo;
        }

        [HttpGet("get/employeeBenefitList")]
        public IActionResult GetBenefitList()
        {
            try
            {
                var benefitList = _mapper.Map<List<EmployeeBenefitDto>>(_employeeBenefitRepo.GetAll());
                if (benefitList == null) return BadRequest("No EmployeeBenefitList found.");
                return Ok(benefitList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/employeeBenefit/{employeeId}")]
        public IActionResult GetBenefitByEmployeeId(string employeeId)
        {
            try
            {
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var get = _mapper.Map<List<EmployeeBenefitDto>>(_employeeBenefitRepo.GetAll().Where(eb => eb.EmployeeId == employeeId));
                if (get == null)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found.");
                }
                return Ok(get);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateBenefit([FromBody] EmployeeBenefitDto employeeBenefit)
        {
            try
            {
                if (employeeBenefit == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!employeeIdRegex.IsMatch(employeeBenefit.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!employeeBenefitIdRegex.IsMatch(employeeBenefit.AllowancesId))
                {
                    return BadRequest("Wrong allowancesId Format.");
                }
                if (!allowanceIdRegex.IsMatch(employeeBenefit.AllowanceId))
                {
                    return BadRequest("Wrong allowancesId Format.");
                }
                var temp = _mapper.Map<EmployeeBenefit>(employeeBenefit);
                _employeeBenefitRepo.Add(temp);
                return Ok(_mapper.Map<EmployeeBenefitDto>(temp));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

       
    }
}
