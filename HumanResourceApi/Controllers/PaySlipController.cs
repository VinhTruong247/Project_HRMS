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
        private readonly EmployeeBenefitRepo _benefitRepo;
        private readonly EmployeeRepo _empRepo;
        private readonly EmployeeContractRepo _contractRepo;
        private readonly AttendanceRepo _attRepo;
        private readonly JobRepo _jobRepo;
        Regex payslipIdRegex = new Regex(@"^PS\d{6}");
        Regex employeeIdRegex = new Regex(@"^EP\d{6}");
        Regex employeeBenefitIdRegex = new Regex(@"^EB\d{6}");
        Regex contractIdRegex = new Regex(@"^PS\d{6}");

        public PaySlipController(IMapper mapper, PaySlipRepo paySlipRepo, EmployeeBenefitRepo benefitRepo, 
            EmployeeRepo empRepo, EmployeeContractRepo contractRepo, AttendanceRepo attRepo, JobRepo jobRepo)
        {
            _mapper = mapper;
            _paySlipRepo = paySlipRepo;
            _benefitRepo = benefitRepo;
            _empRepo = empRepo;
            _contractRepo = contractRepo;
            _attRepo = attRepo;
            _jobRepo = jobRepo;
        }

        [HttpGet("get/paysliplist")]
        public IActionResult GetPaySlipList()
        {
            try
            {
                var payslipList = _mapper.Map<List<PaySlipDto>>(_paySlipRepo.GetAll());
                if (payslipList == null) return BadRequest("No PaySlipList found.");
                payslipList.ForEach(x =>
                {
                    x.Tax = _paySlipRepo.GetTax(x.TaxIncome);
                });
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

        [HttpPost("generate")]
        public IActionResult GeneratePayslip([FromBody] PaySlipRequestModel requestModel)
        {
            try
            {
                if (!_empRepo.GetAll().Any(e => e.EmployeeId == requestModel.EmployeeId))
                {
                    return BadRequest("No employee found");
                }
                int count = _paySlipRepo.GetAll().Count() + 1;
                var payslipId = "PS" + count.ToString().PadLeft(6, '0');

                var tempContract = _contractRepo.GetAll().Where(c => c.EmployeeId == requestModel.EmployeeId).FirstOrDefault();
                var baseSalary = tempContract.BaseSalary;

                //OT not complete
                var otHours = TimeSpan.Zero;
                string contractId = tempContract.ContractId;
                TimeSpan? standardWorkHours = TimeSpan.FromHours(8 * 22);
                TimeSpan? actualWorkHours = _attRepo.GetActualHours(requestModel.EmployeeId, requestModel.PaidDate ?? DateTime.Now);
                decimal taxIncome = _paySlipRepo.GetTaxIncome(requestModel.EmployeeId, requestModel.PaidDate ?? DateTime.Now);
                decimal? bonus = _jobRepo.GetBonus(requestModel.EmployeeId);
                decimal? totalSalary = _paySlipRepo.GetTotalSalary(taxIncome,
                    _benefitRepo.GetAllowanceSum(requestModel.EmployeeId),
                    _paySlipRepo.GetTax(taxIncome));
                var payslip = _mapper.Map<PaySlip>(requestModel);
                payslip.PayslipId = payslipId;
                payslip.BaseSalary = baseSalary;
                payslip.OtHours = otHours;
                payslip.ContractId = contractId;
                payslip.StandardWorkHours = TimeSpan.Zero;
                payslip.ActualWorkHours = actualWorkHours;
                payslip.TaxIncome = taxIncome;
                payslip.Bonus = bonus;
                payslip.TotalSalary = totalSalary;

                _paySlipRepo.Add(payslip);
                var mappedPayslip = _mapper.Map<PaySlipDto>(payslip);
                mappedPayslip.Tax = _paySlipRepo.GetTax(mappedPayslip.TaxIncome);
                return Ok(mappedPayslip);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
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
        [HttpGet("payslip")]
        public IActionResult GetPayslip(decimal taxIncome)
        {
            // return Ok(_benefitRepo.GetAllowanceSum(employeeId));
            return Ok(_paySlipRepo.GetTax(taxIncome));
        }
    }
}
