using AutoMapper;
using HumanResourceApi.DTO.PaySlip;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaySlipController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly PaySlipRepo _paySlipRepo;
        Regex payslipIdRegex = new Regex(@"^PS\d{6}");
        Regex employeeIdRegex = new Regex(@"^EP\d{6}");
        Regex employeeBenefitIdRegex = new Regex(@"^EB\d{6}");
        Regex contractIdRegex = new Regex(@"^PS\d{6}");

        public PaySlipController(IMapper mapper, PaySlipRepo paySlipRepo)
        {
            _mapper = mapper;
            _paySlipRepo = paySlipRepo;
        }

        [HttpGet("get/paysliplist")]
        public IActionResult GetPaySlipList()
        {
            try
            {
                var payslipList = _mapper.Map<List<PaySlipDto>>(_paySlipRepo.GetAll());
                if (payslipList == null) return BadRequest("No PaySlipList found.");
                return Ok(payslipList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/payslip/{employeeId}")]
        public IActionResult GetPaySlipDetailById(string employeeId)
        {
            try
            {
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var get = _mapper.Map<PaySlipDto>(_paySlipRepo.GetAll().Where(ps => ps.EmployeeId == employeeId).FirstOrDefault());
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
        public IActionResult CreatePaySlip([FromBody] PaySlipDto payslip)
        {
            try
            {
                if (payslip == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!payslipIdRegex.IsMatch(payslip.PayslipId))
                {
                    return BadRequest("Wrong PaySlipId Format.");
                }
                if (!employeeBenefitIdRegex.IsMatch(payslip.AllowancesId))
                {
                    return BadRequest("Wrong EmployeeBenefitId Format.");
                }
                if (!employeeIdRegex.IsMatch(payslip.EmployeeId))
                {
                    return BadRequest("Wrong EmployeeId Format.");
                }
                if (!contractIdRegex.IsMatch(payslip.ContractId))
                {
                    return BadRequest("Wrong ContractId Format.");
                }
                if (_paySlipRepo.GetAll().Any(ps => ps.PayslipId == payslip.PayslipId))
                {
                    return BadRequest("PaySlip ID = " + payslip.PayslipId + " existed");
                }
                var temp = _mapper.Map<PaySlip>(payslip);
                _paySlipRepo.Add(temp);
                return Ok(_mapper.Map<PaySlipDto>(temp));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/payslip/{employeeId}/{payslipId}")]
        public IActionResult UpdatePaySlip(string employeeId, string payslipId, [FromBody] UpdatePaySlipDto payslip)
        {
            try
            {
                if (payslip == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!payslipIdRegex.IsMatch(payslipId))
                {
                    return BadRequest("Wrong PaySlipId Format.");
                }
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong EmployeeId Format.");
                }
                if (!contractIdRegex.IsMatch(payslip.ContractId))
                {
                    return BadRequest("Wrong ContractId Format.");
                }
                var valid = _paySlipRepo.GetAll().Where(ps => ps.PayslipId == payslipId && ps.EmployeeId == employeeId).FirstOrDefault();
                if (valid == null)
                {
                    return BadRequest("PaySlip ID = " + payslipId + " doesn't seem to be found.");
                }
                _mapper.Map(payslip, valid);
                valid.PayslipId = payslipId;
                valid.EmployeeId = employeeId;

                _paySlipRepo.Update(valid);
                return Ok(_mapper.Map<PaySlipDto>(valid));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
