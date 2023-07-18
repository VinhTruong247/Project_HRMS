using AutoMapper;
using HumanResourceApi.DTO.Attendance;
using HumanResourceApi.DTO.Timesheet;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly AttendanceRepo _attendance;
        public readonly EmployeeRepo _employee;
        public readonly TimesheetRepo _timesheet;
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public readonly TimeSpan workTime = new TimeSpan(08, 00, 00);

        public AttendanceController(IMapper mapper, AttendanceRepo attendance, EmployeeRepo employee, TimesheetRepo timesheet)
        {
            _mapper = mapper;
            _attendance = attendance;
            _employee = employee;
            _timesheet = timesheet;
        }

        [SwaggerOperation(Summary = "get list of attendances")]
        [HttpGet("attendances")]
        public IActionResult GetAttendance()
        {
            try
            {
                var attendanceList = _mapper.Map<List<AttendanceDto>>(_attendance.GetAll().ToList());
                return Ok(attendanceList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{date}/attendances")]
        public IActionResult GetAttendancesByDate(DateTime date)
        {
            try
            {
                var attendanceList = _mapper.Map<List<AttendanceDto>>(_attendance.GetAll().Where(a => a.Day == date).ToList());
                return Ok(attendanceList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/attendance/{employeeId}")]
        public IActionResult GetAttendanceId(string employeeId)
        {
            var attendance = _mapper.Map<AttendanceDto>(_attendance.GetAll().Where(a => a.EmployeeId == employeeId).FirstOrDefault());
            if (attendance == null)
            {
                return BadRequest();
            }
            return Ok(attendance);
        }

        [SwaggerOperation(Summary =
            "Create a new attendance for the employee and late hour will be calculated automatically, if note will contain \"Arrived late\"")]
        [HttpPost("create/{employeeId}")]
        public IActionResult CreateAttendance(string employeeId)
        {
            try
            {
                string note = "";
                int count = _attendance.GetAll().Count() + 1;
                var attendanceId = "AT" + count.ToString().PadLeft(6, '0');
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (_employee.GetAll().Any(e => e.EmployeeId == employeeId) == false)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found");
                }
                var today = DateTime.Now.Date;
                var timeIn = DateTime.Now.TimeOfDay;
                var timeOut = new TimeSpan(00, 00, 00);
                var lateHours = timeIn - workTime;
                var earlyLeaveTime = new TimeSpan(00, 00, 00);
                var totalHours = new TimeSpan(00, 00, 00);
                bool attendanceStatus = true;
                if (lateHours > new TimeSpan(00, 00, 00))
                {
                    note = "Arrived late";
                }
                else
                {
                    note = "Arrived early/on time";
                }
                Attendance temp = new Attendance
                {
                    AttendanceId = attendanceId,
                    EmployeeId = employeeId,
                    AttendanceStatus = attendanceStatus,
                    Day = today,
                    EarlyLeaveHours = earlyLeaveTime,
                    LateHours = lateHours,
                    Notes = note,
                    TimeIn = timeIn,
                    TimeOut = timeOut,
                    TotalHours = totalHours
                };

                //create timesheet for the first time of the day
                if (!_timesheet.GetAll().Any(t => t.EmployeeId == employeeId && t.Day == today))
                {
                    int countTimesheet = _timesheet.GetAll().Count() + 1;
                    var timesheetId = "TS" + countTimesheet.ToString().PadLeft(6, '0');
                    Timesheet timesheet = new Timesheet
                    {
                        TimesheetId = timesheetId,
                        EmployeeId = employeeId,
                        TimeIn = timeIn,
                        TimeOut = timeOut,
                        Day = today,
                        Status = true,
                        TotalWorkHours = totalHours
                    };
                    _timesheet.Add(timesheet);
                }

                _attendance.Add(temp);
                return Ok(_mapper.Map<AttendanceDto>(temp));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }


        [HttpPut("update/attendance/{employeeId}")]
        public IActionResult UpdateAttendance(string employeeId)
        {
            try
            {
                if (!employeeIdRegex.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (_employee.GetAll().Any(e => e.EmployeeId == employeeId) == false)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found");
                }
                var attendanceList = _mapper.Map<List<AttendanceDto>>(_attendance.GetAll().Where(a => a.EmployeeId == employeeId).OrderByDescending(a => a.AttendanceId));
                var edit = attendanceList[0];
                edit.TimeOut = DateTime.Now.TimeOfDay;
                if (new TimeSpan(17, 00, 00) - edit.TimeOut > new TimeSpan(00, 00, 00))
                {
                    edit.EarlyLeaveHours = new TimeSpan(17, 00, 00) - edit.TimeOut;
                } else
                {
                    edit.EarlyLeaveHours = new TimeSpan(00, 00, 00);
                }
                edit.TotalHours = edit.TimeOut - edit.TimeIn;
                var valid = _attendance.GetAll().Where(a => a.EmployeeId == employeeId).OrderByDescending(a => a.AttendanceId).ToList()[0];
                _mapper.Map(edit,valid);

                //update timesheet of the day
                if (_timesheet.GetAll().Any(t => t.EmployeeId == employeeId && t.Day == edit.Day))
                {
                    var timesheet = _mapper.Map<TimesheetDto>(_timesheet.GetAll().Where(t => t.EmployeeId == employeeId && t.Day == edit.Day).FirstOrDefault());
                    timesheet.TimeOut = edit.TimeOut;
                    timesheet.TotalWorkHours += edit.TotalHours;
                    var validTimesheet = _timesheet.GetAll().Where(t => t.EmployeeId == employeeId && t.Day == edit.Day).FirstOrDefault();
                    _mapper.Map(timesheet, validTimesheet);
                    _timesheet.Update(validTimesheet);
                }

                _attendance.Update(valid);
                return Ok(edit);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
        //[Authorize]
        //[HttpPost("delete")]
        //public IActionResult DeleteAttendance([FromQuery] string id)
        //{
        //    var attendance = _mapper.Map<AttendanceDto>(_attendance.GetAll().Where(a => a.EmployeeId == id).FirstOrDefault());
        //    if (attendance == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (attendance.AttendanceStatus == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validAttendance = _attendance.GetAll().Where(a => a.EmployeeId == id).FirstOrDefault();
        //    _mapper.Map(attendance, validAttendance);
        //    validAttendance.AttendanceStatus = "Disable";

        //    _attendance.Update(validAttendance);
        //    return Ok(validAttendance);
        //}
    }
}
