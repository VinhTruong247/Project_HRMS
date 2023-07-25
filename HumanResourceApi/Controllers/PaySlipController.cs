using AutoMapper;
using HumanResourceApi.DTO.DailySalary;
using HumanResourceApi.DTO.PaySlip;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaySlipController : ControllerBase
    {
        private readonly decimal giamTruGiaCanh = 11000000;
        private readonly decimal mucGiamTruGiaCanhNguoiPhuThuoc = 4400000;
        public readonly IMapper _mapper;
        public readonly PaySlipRepo _paySlipRepo;
        private readonly EmployeeBenefitRepo _benefitRepo;
        private readonly EmployeeRepo _empRepo;
        private readonly EmployeeContractRepo _contractRepo;
        private readonly AttendanceRepo _attRepo;
        private readonly JobRepo _jobRepo;
        private readonly OvertimeRepo _otRepo;
        private readonly DailySalaryRepo _dailySalaryRepo;
        Regex payslipIdRegex = new Regex(@"^PS\d{6}");
        Regex employeeIdRegex = new Regex(@"^EP\d{6}");
        Regex employeeBenefitIdRegex = new Regex(@"^EB\d{6}");
        Regex contractIdRegex = new Regex(@"^CN\d{6}");

        public PaySlipController(IMapper mapper, PaySlipRepo paySlipRepo, EmployeeBenefitRepo benefitRepo,
            EmployeeRepo empRepo, EmployeeContractRepo contractRepo, AttendanceRepo attRepo, JobRepo jobRepo,
            OvertimeRepo otRepo, DailySalaryRepo dailySalaryRepo)
        {
            _mapper = mapper;
            _paySlipRepo = paySlipRepo;
            _benefitRepo = benefitRepo;
            _empRepo = empRepo;
            _contractRepo = contractRepo;
            _attRepo = attRepo;
            _jobRepo = jobRepo;
            _otRepo = otRepo;
            _dailySalaryRepo = dailySalaryRepo;
        }

        [HttpGet("get/paysliplist")]
        public IActionResult GetPaySlipList()
        {
            try
            {
                var payslipList = _mapper.Map<List<PaySlipDto>>(_paySlipRepo.GetAll());
                if (payslipList == null) return BadRequest("No PaySlipList found.");
                payslipList.ForEach(responsePayslip =>
                {
                    responsePayslip.DailyAllowanceSum = _dailySalaryRepo.GetAllowance(responsePayslip.EmployeeId, responsePayslip.PaidDate.AddMonths(-1), _benefitRepo.GetDailyAllowance(responsePayslip.EmployeeId)) ;
                    responsePayslip.MonthlyAllowanceSum = _benefitRepo.GetMonthlyAllowance(responsePayslip.EmployeeId); 
                    responsePayslip.MonthylyAllowanceList = _mapper.Map<List<MonthlyAllowance>>(_benefitRepo.GetMonthlyAllowanceDetail(responsePayslip.EmployeeId));
                });
                return Ok(payslipList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/payslip/{employeeId}/{date}")]
        public IActionResult GetPaySlipDetailById(string employeeId, DateTime date)
        {
            try
            {
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var get = _mapper.Map<PaySlipDto>(_paySlipRepo.GetAll().Where(ps => ps.EmployeeId == employeeId && ps.PaidDate.Month == date.AddMonths(1).Month).FirstOrDefault());
                if (get == null)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found.");
                }
                get.DailyAllowanceSum = _dailySalaryRepo.GetAllowance(get.EmployeeId, get.PaidDate.AddMonths(-1), _benefitRepo.GetDailyAllowance(get.EmployeeId));
                get.MonthlyAllowanceSum = _benefitRepo.GetMonthlyAllowance(get.EmployeeId);
                get.MonthylyAllowanceList = _mapper.Map<List<MonthlyAllowance>>(_benefitRepo.GetMonthlyAllowanceDetail(get.EmployeeId));
                return Ok(get);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }


        [SwaggerOperation(Summary = "muc bao hiem hien gio: 10.5% => 0.105")]
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
                //get the missing data
                decimal baseSalaryPerHour = tempEmp.Job.BaseSalaryPerHour ?? 0;
                int bankAccountNumber = tempEmp.BankAccountNumber ?? 0;
                string bankAccountName = tempEmp.BankAccountName;
                string bankName = tempEmp.BankName;
                string contractId = tempContract.ContractId;
                decimal standardWorkHours = 8 * 22;
                decimal actualWorkHours = _dailySalaryRepo.GetMonthlyHour(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                var otHours = _dailySalaryRepo.GetMonthlyOtHour(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                var baseSalary = _dailySalaryRepo.GetBaseSalary(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                decimal otSalary = _dailySalaryRepo.GetOtSalary(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1));
                decimal dailyAllowance = _benefitRepo.GetDailyAllowance(requestModel.EmployeeId);
                decimal dailyAllowanceSum = _dailySalaryRepo.GetAllowance(requestModel.EmployeeId, requestModel.PaidDate.AddMonths(-1), dailyAllowance);
                decimal allowanceSum = _benefitRepo.GetAllowanceSum(requestModel.EmployeeId, dailyAllowanceSum);
                decimal totalBeforeDeduction = baseSalary + otSalary + allowanceSum;
                decimal bhytAmount = totalBeforeDeduction * (decimal)requestModel.BhytPercentage;
                int dependent = tempEmp.Dependents ?? 0;
                decimal giamTruGiaCanhNguoiPhuThuoc = dependent * mucGiamTruGiaCanhNguoiPhuThuoc;
                decimal thuNhapTruocThue = totalBeforeDeduction - bhytAmount;
                decimal taxIncome = thuNhapTruocThue - giamTruGiaCanh - giamTruGiaCanhNguoiPhuThuoc;
                if (taxIncome < 0)
                    taxIncome = 0;
                decimal tax = _paySlipRepo.GetTax(taxIncome);
                decimal? totalSalary = thuNhapTruocThue - tax;


                //insert missing data to payslip
                var payslip = _mapper.Map<PaySlip>(requestModel);
                payslip.PayslipId = payslipId;
                payslip.StandardWorkHours = standardWorkHours;
                payslip.ActualWorkHours = actualWorkHours;
                payslip.OtHours = otHours;
                payslip.Dependent = dependent;
                payslip.BaseSalaryPerHour = baseSalaryPerHour;
                payslip.BaseSalary = baseSalary;
                payslip.OtSalary = otSalary;
                payslip.Allowance = allowanceSum;
                payslip.TotalBeforeDeduction = totalBeforeDeduction;
                payslip.BhytAmount = bhytAmount;
                payslip.GiamTruGiaCanh = giamTruGiaCanh;
                payslip.GiamTruGiaCanhNguoiPhuThuoc = giamTruGiaCanhNguoiPhuThuoc;
                payslip.TaxIncome = taxIncome;
                payslip.Tax = tax;
                payslip.TotalSalary = totalSalary;
                payslip.BankAccountNumber = bankAccountNumber;
                payslip.BankAccountName = bankAccountName;
                payslip.BankName = bankName;
                payslip.Status = "Pending";
                payslip.ContractId = contractId;

                _paySlipRepo.Add(payslip);
                var mappedPayslip = _mapper.Map<PaySlipDto>(payslip);
                mappedPayslip.DailyAllowanceSum = _dailySalaryRepo.GetAllowance(mappedPayslip.EmployeeId, mappedPayslip.PaidDate.AddMonths(-1), _benefitRepo.GetDailyAllowance(mappedPayslip.EmployeeId));
                mappedPayslip.MonthlyAllowanceSum = _benefitRepo.GetMonthlyAllowance(mappedPayslip.EmployeeId);
                mappedPayslip.MonthylyAllowanceList = _mapper.Map<List<MonthlyAllowance>>(_benefitRepo.GetMonthlyAllowanceDetail(mappedPayslip.EmployeeId));
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
                responsePayslip.DailyAllowanceSum = _dailySalaryRepo.GetAllowance(responsePayslip.EmployeeId, responsePayslip.PaidDate.AddMonths(-1), _benefitRepo.GetDailyAllowance(responsePayslip.EmployeeId));
                responsePayslip.MonthlyAllowanceSum = _benefitRepo.GetMonthlyAllowance(responsePayslip.EmployeeId);
                responsePayslip.MonthylyAllowanceList = _mapper.Map<List<MonthlyAllowance>>(_benefitRepo.GetMonthlyAllowanceDetail(responsePayslip.EmployeeId));
                return Ok(responsePayslip);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
