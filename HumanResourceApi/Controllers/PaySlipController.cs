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
        private readonly OvertimeRepo _otRepo;
        Regex payslipIdRegex = new Regex(@"^PS\d{6}");
        Regex employeeIdRegex = new Regex(@"^EP\d{6}");
        Regex employeeBenefitIdRegex = new Regex(@"^EB\d{6}");
        Regex contractIdRegex = new Regex(@"^CN\d{6}");

        public PaySlipController(IMapper mapper, PaySlipRepo paySlipRepo, EmployeeBenefitRepo benefitRepo, 
            EmployeeRepo empRepo, EmployeeContractRepo contractRepo, AttendanceRepo attRepo, JobRepo jobRepo,
            OvertimeRepo otRepo)
        {
            _mapper = mapper;
            _paySlipRepo = paySlipRepo;
            _benefitRepo = benefitRepo;
            _empRepo = empRepo;
            _contractRepo = contractRepo;
            _attRepo = attRepo;
            _jobRepo = jobRepo;
            _otRepo = otRepo;
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
                    x.Allowance = _benefitRepo.GetAllowanceSum(x.EmployeeId, x.ActualWorkHours ?? 0);
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
                var empPaySlips = _paySlipRepo.GetAll().Where(p => p.EmployeeId == requestModel.EmployeeId).ToList();
                var tempEmp = _empRepo.GetAll().Where(e => e.EmployeeId == requestModel.EmployeeId).FirstOrDefault();
                if (tempEmp is null)
                {
                    return BadRequest("No employee found");
                }
                if(empPaySlips.Any(p => p.PaidDate.Month == requestModel.PaidDate.Month && p.PaidDate.Year == requestModel.PaidDate.Year))
                {
                    return BadRequest("Already created payslip for this month");
                }
                int count = _paySlipRepo.GetAll().Count() + 1;
                var payslipId = "PS" + count.ToString().PadLeft(6, '0');
                var tempContract = _contractRepo.GetAll().Where(c => c.EmployeeId == requestModel.EmployeeId).FirstOrDefault();

                //get the missing data
                var otHours = _otRepo.GetOTHours(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                string contractId = tempContract.ContractId;
                decimal standardWorkHours = 8 * 22;
                decimal actualWorkHours = _attRepo.GetActualHours(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                actualWorkHours = Math.Round(actualWorkHours, 2);
                var baseSalary = _jobRepo.GetBaseSalary(tempEmp.EmployeeId, actualWorkHours);
                decimal otSalary = _otRepo.GetOtSalary(otHours, requestModel.EmployeeId);
                decimal taxIncome = _paySlipRepo.GetTaxIncome(baseSalary, otSalary, tempEmp.Dependents ?? 0);
                decimal? bonus = _jobRepo.GetBonus(requestModel.EmployeeId);
                decimal? totalSalary = _paySlipRepo.GetTotalSalary(baseSalary,
                    _benefitRepo.GetAllowanceSum(requestModel.EmployeeId, actualWorkHours),
                    _paySlipRepo.GetTax(taxIncome),
                    otSalary);


                //insert missing data to payslip
                var payslip = _mapper.Map<PaySlip>(requestModel);
                payslip.PayslipId = payslipId;
                payslip.BaseSalary = baseSalary;
                payslip.OtHours = otHours;
                payslip.ContractId = contractId;
                payslip.StandardWorkHours = standardWorkHours;
                payslip.ActualWorkHours = actualWorkHours;
                payslip.TaxIncome = taxIncome;
                payslip.TotalSalary = totalSalary;

                _paySlipRepo.Add(payslip);
                var mappedPayslip = _mapper.Map<PaySlipDto>(payslip);
                mappedPayslip.Tax = _paySlipRepo.GetTax(mappedPayslip.TaxIncome);
                mappedPayslip.Allowance = _benefitRepo.GetAllowanceSum(requestModel.EmployeeId, actualWorkHours);
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
    }
}
