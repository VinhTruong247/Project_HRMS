using AutoMapper;
using HumanResourceApi.DTO.Report;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ReportRepo _reportRepo;
        public Regex reportIdRegex = new Regex(@"^RP\d{6}");
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public ReportController(IMapper mapper, ReportRepo reportRepo)
        {
            _mapper = mapper;
            _reportRepo = reportRepo;
        }

        [HttpGet("get/reports")]
        public IActionResult GetReportList()
        {
            try
            {
                var reportList = _mapper.Map<List<ReportDto>>(_reportRepo.GetAll());
                if (reportList == null)
                {
                    return BadRequest("No Report(s) found.");
                }
                return Ok(reportList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/report/{employeeId}")]
        public IActionResult GetReportsOfEmployee(string employeeId)
        {
            try
            {
                var reportList = _mapper.Map<List<ReportDto>>(_reportRepo.GetAll().Where(rp => rp.EmployeeId == employeeId));
                if (reportList == null)
                {
                    return BadRequest("No Report(s) found.");
                }
                return Ok(reportList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/report/{employeeId}/{reportId}")]
        public IActionResult GetDetailReport(string employeeId, string reportId)
        {
            try
            {
                var report = _mapper.Map<ReportDto>(_reportRepo.GetAll().Where(rp => rp.EmployeeId == employeeId && rp.ReportId == reportId).FirstOrDefault());
                if (report == null)
                {
                    return BadRequest("Report ID = " + reportId + " doesn't seem to be found");
                }
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateReport([FromBody] CreateReportDto report)
        {
            try
            {
                if (report == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!employeeIdRegex.IsMatch(report.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                int count = _reportRepo.GetAll().Count() + 1;
                var reportId = "RP" + count.ToString().PadLeft(6, '0');
                var temp = _mapper.Map<Report>(report);
                temp.IssueDate = DateTime.Now;
                temp.Status = "Pending";
                temp.ReportId = reportId;

                _reportRepo.Add(temp);
                return Ok(_mapper.Map<ReportDto>(temp));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/report/{employeeId}/{reportId}")]
        public IActionResult UpdateReport(string employeeId, string reportId, [FromBody] UpdateReportDto report)
        {
            try
            {
                if (string.IsNullOrEmpty(reportId) || string.IsNullOrEmpty(employeeId) || report == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!reportIdRegex.IsMatch(reportId))
                {
                    return BadRequest("Wrong reportId Format.");
                }
                var validReport = _reportRepo.GetAll().Where(rp => rp.EmployeeId == employeeId && rp.ReportId == reportId).FirstOrDefault();
                if (validReport == null)
                {
                    return BadRequest("Report ID = " + reportId + " doesn't seem to be found.");
                }
                _mapper.Map(report, validReport);
                validReport.EmployeeId = employeeId;
                validReport.ReportId = reportId;

                _reportRepo.Update(validReport);
                return Ok(_mapper.Map<ReportDto>(validReport));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [SwaggerOperation(Summary = "use date to find reports")]
        [HttpGet("report/search/{date}")]
        public IActionResult FindEmployee(DateTime date)
        {
            try
            {
                var resultList = _mapper.Map<List<ReportDto>>(_reportRepo.GetAll().Where(rp => rp.IssueDate.Month == date.Month));
                if (resultList == null)
                {
                    return BadRequest("No report(s) found");
                }
                return Ok(resultList.OrderByDescending(r => r.IssueDate));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
