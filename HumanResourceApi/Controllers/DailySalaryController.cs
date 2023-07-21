using AutoMapper;
using HumanResourceApi.DTO.DailySalary;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailySalaryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DailySalaryRepo _dailySalaryRepo;
        private readonly EmployeeBenefitRepo _employeeBenefitRepo;

        public DailySalaryController(IMapper mapper, DailySalaryRepo dailySalaryRepo, EmployeeBenefitRepo employeeBenefitRepo)
        {
            _mapper = mapper;
            _dailySalaryRepo = dailySalaryRepo;
            _employeeBenefitRepo = employeeBenefitRepo;
        }
        [HttpGet("dailysalaries")]
        public IActionResult GetDailySalaries() 
        {
            try
            {
                var dailySalaryList = _mapper.Map<List<ResponseDailySalary>>(_dailySalaryRepo.GetAll().ToList());
                dailySalaryList.ForEach(dailySalary =>
                {
                    decimal dailyAllowance = _employeeBenefitRepo.GetDailyAllowance(dailySalary.EmployeeId);
                    dailySalary.DailyAllowance = dailyAllowance;
                    dailySalary.DailySalary = _dailySalaryRepo.GetDailySalary((decimal)dailySalary.TotalHours.TotalHours, dailySalary.SalaryPerHour ?? 0, dailySalary.OtSalary ?? 0, dailyAllowance);
                    dailySalary.DailyAllowanceList =_mapper.Map<List<DailyAllowance>>(_employeeBenefitRepo.GetDailyAllowanceDetail(dailySalary.EmployeeId));
                    
                });
                return Ok(dailySalaryList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("employee/date/dailysalaries")]
        public IActionResult GetDailySalariesForAnEmployeeInAMonth([FromBody] RequestDailySalary request)
        {
            try
            {
                var dailySalaryList = _mapper.Map<List<ResponseDailySalary>>(_dailySalaryRepo.GetAll()
                    .Where(d => d.EmployeeId == request.EmployeeId && d.Date.Month == request.Date.Month)
                    .ToList());
                dailySalaryList.ForEach(dailySalary =>
                {
                    decimal dailyAllowance = _employeeBenefitRepo.GetDailyAllowance(dailySalary.EmployeeId);
                    dailySalary.DailyAllowance = dailyAllowance;
                    dailySalary.DailySalary = _dailySalaryRepo.GetDailySalary((decimal)dailySalary.TotalHours.TotalHours, dailySalary.SalaryPerHour ?? 0, dailySalary.OtSalary ?? 0, dailyAllowance);
                    dailySalary.DailyAllowanceList = _mapper.Map<List<DailyAllowance>>(_employeeBenefitRepo.GetDailyAllowanceDetail(dailySalary.EmployeeId));
                });
                return Ok(dailySalaryList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
