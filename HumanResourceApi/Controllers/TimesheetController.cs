using AutoMapper;
using HumanResourceApi.DTO.Attendance;
using HumanResourceApi.DTO.Timesheet;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController :ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly TimesheetRepo _sheetRepo;
        public readonly AttendanceRepo _attendanceRepo;

        public TimesheetController(IMapper mapper, TimesheetRepo sheetRepo)
        {
            _mapper = mapper;
            _sheetRepo = sheetRepo;
        }

        [HttpGet("get/timesheets")]
        public IActionResult GetAllTimesheet()
        {
            try
            {
                var timesheetList = _mapper.Map<List<TimesheetDto>>(_sheetRepo.GetAll());
                if (timesheetList == null)
                {
                    return BadRequest("There's no active timesheet");
                }
                return Ok(timesheetList.OrderByDescending(x => x.Day));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/timesheet/{employeeId}")]
        public IActionResult GetEmployeeTimesheet(string employeeId)
        {
            try
            {
                var timesheetList = _mapper.Map<List<TimesheetDto>>(_sheetRepo.GetAll().Where(t => t.EmployeeId == employeeId));
                if (timesheetList == null)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found");
                }
                return Ok(timesheetList.OrderByDescending(x => x.Day));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
