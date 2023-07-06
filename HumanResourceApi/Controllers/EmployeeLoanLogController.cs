using AutoMapper;
using HumanResourceApi.DTO.EmployeeLoanLog;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeLoanLogController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeLoanLogRepo _loanLog;
        public Regex loanIdRegex = new Regex(@"^LN\d{6}");
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public EmployeeLoanLogController(IMapper mapper, EmployeeLoanLogRepo loanLog)
        {
            _mapper = mapper;
            _loanLog = loanLog;
        }

        [HttpGet("get/loanLogs")]
        public IActionResult GetAllLoanLogs()
        {
            try
            {
                var log = _mapper.Map<List<LoanLogDto>>(_loanLog.GetAll());
                if (log == null)
                {
                    return BadRequest("There's no loan log");
                }
                return Ok(log);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/loanLog/{loanId}")]
        public IActionResult GetLoanLogById(string loanId)
        {
            try
            {
                if (!loanIdRegex.IsMatch(loanId))
                {
                    return BadRequest("Wrong loanId Format.");
                }
                var result = _mapper.Map<LoanLogDto>(_loanLog.GetAll().Where(l => l.LoanId == loanId).FirstOrDefault());
                if (result == null)
                {
                    return BadRequest("Loan ID = " + loanId + " doesn't seem to be found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] LoanLogDto log)
        {
            try
            {
                if (log == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!loanIdRegex.IsMatch(log.LoanId))
                {
                    return BadRequest("Wrong loanId Format.");
                }
                if (!employeeIdRegex.IsMatch(log.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (_loanLog.GetAll().Any(l => l.LoanId == log.LoanId))
                {
                    return BadRequest("Loan ID = " + log.LoanId + " existed");
                }
                var temp = _mapper.Map<EmployeeLoanLog>(log);
                _loanLog.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/loanLog/{loanId}")]
        public IActionResult UpdateLoanLog(string loanId, [FromBody] UpdateLoanLogDto log)
        {
            try
            {
                if (log == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!loanIdRegex.IsMatch(loanId))
                {
                    return BadRequest("Wrong loanId Format.");
                }
                if (!employeeIdRegex.IsMatch(log.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var valid = _loanLog.GetAll().Where(l => l.LoanId == loanId).FirstOrDefault();
                if (valid == null)
                {
                    return BadRequest("Loan ID = " + loanId + " doesn't seem to exist.");
                }
                _mapper.Map(log, valid);
                valid.LoanId = loanId;

                _loanLog.Update(valid);
                return Ok(valid);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
