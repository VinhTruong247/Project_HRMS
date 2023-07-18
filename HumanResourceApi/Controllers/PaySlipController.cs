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
                    var tempEmp = _empRepo.GetAnEmployee(x.EmployeeId);
                    x.Tax = _paySlipRepo.GetTax(x.TaxIncome);
                    x.Allowance = _benefitRepo.GetAllowanceSum(x.EmployeeId, x.ActualWorkHours ?? 0);
                    x.OtSalary = _otRepo.GetOtSalary(x.OtHours ?? 0, x.EmployeeId);
                    x.BaseSalaryPerHour = tempEmp.Job.BaseSalaryPerHour ?? 0;
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
                var get = _mapper.Map<PaySlipDto>(_paySlipRepo.GetAll().Where(ps => ps.EmployeeId == employeeId && ps.PaidDate.Month == DateTime.Now.AddMonths(1).Month).FirstOrDefault());
                if (get == null)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found.");
                }
                var tempEmp = _empRepo.GetAnEmployee(get.EmployeeId);
                get.Tax = _paySlipRepo.GetTax(get.TaxIncome);
                get.Allowance = _benefitRepo.GetAllowanceSum(get.EmployeeId, get.ActualWorkHours ?? 0);
                get.OtSalary = _otRepo.GetOtSalary(get.OtHours ?? 0, get.EmployeeId);
                get.BaseSalaryPerHour = tempEmp.Job.BaseSalaryPerHour ?? 0;
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
                var tempEmp = _empRepo.GetAnEmployee(requestModel.EmployeeId);
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
                decimal baseSalaryPerHour = tempEmp.Job.BaseSalaryPerHour ?? 0;
                int bankAccountNumber = tempEmp.BankAccountNumber ?? 0;
                string bankAccountName = tempEmp.BankAccountName;
                string bankName = tempEmp.BankName;


                //get the missing data
                var otHours = _otRepo.GetOTHours(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                string contractId = tempContract.ContractId;
                decimal standardWorkHours = 8 * 22;
                decimal actualWorkHours = _attRepo.GetActualHours(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                actualWorkHours = Math.Round(actualWorkHours, 2);
                var baseSalary = _jobRepo.GetBaseSalary(tempEmp.EmployeeId, actualWorkHours);
                decimal otSalary = _otRepo.GetOtSalary(otHours, requestModel.EmployeeId);
                decimal allowanceSum = _benefitRepo.GetAllowanceSum(requestModel.EmployeeId, actualWorkHours);
                decimal taxIncome = _paySlipRepo.GetTaxIncome(baseSalary, otSalary, tempEmp.Dependents ?? 0, allowanceSum);
                decimal tax = _paySlipRepo.GetTax(taxIncome);
                decimal? totalSalary = _paySlipRepo.GetTotalSalary(baseSalary, allowanceSum, tax, otSalary);


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
                payslip.BankAccountNumber = bankAccountNumber;
                payslip.BankAccountName = bankAccountName;
                payslip.BankName = bankName;
                payslip.Status = "Pending";

                _paySlipRepo.Add(payslip);
                var mappedPayslip = _mapper.Map<PaySlipDto>(payslip);
                mappedPayslip.Tax = tax;
                mappedPayslip.Allowance = allowanceSum;
                mappedPayslip.OtSalary = otSalary;
                mappedPayslip.BaseSalaryPerHour = baseSalaryPerHour;
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
                var valid = _paySlipRepo.GetAll().Where(ps => ps.PayslipId == payslipId && ps.EmployeeId == employeeId).FirstOrDefault();
                if (valid == null)
                {
                    return BadRequest("PaySlip ID = " + payslipId + " doesn't seem to be found.");
                }
                _mapper.Map(payslip, valid);
                valid.PayslipId = payslipId;
                valid.EmployeeId = employeeId;

                _paySlipRepo.Update(valid);
                var responsePayslip = _mapper.Map<PaySlipDto>(valid);
                var tempEmp = _empRepo.GetAnEmployee(valid.EmployeeId);
                responsePayslip.Tax = _paySlipRepo.GetTax(responsePayslip.TaxIncome);
                responsePayslip.Allowance = _benefitRepo.GetAllowanceSum(responsePayslip.EmployeeId, responsePayslip.ActualWorkHours ?? 0);
                responsePayslip.OtSalary = _otRepo.GetOtSalary(responsePayslip.OtHours ?? 0, responsePayslip.EmployeeId);
                responsePayslip.BaseSalaryPerHour = tempEmp.Job.BaseSalaryPerHour ?? 0;
                return Ok(responsePayslip);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
