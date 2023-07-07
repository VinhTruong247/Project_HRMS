using AutoMapper;
using HumanResourceApi.DTO.Attendance;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly AttendanceRepo _attendance;

        public AttendanceController(IMapper mapper, AttendanceRepo attendance)
        {
            _mapper = mapper;
            _attendance = attendance;
        }

        [SwaggerOperation(Summary = "get list of attendances")]
        [HttpGet("attendances")]
        public IActionResult GetAttendance()
        {
            try
            {
                var attendanceList = _mapper.Map<List<AttendanceDto>>(_attendance.GetAll().ToList());
                return Ok(attendanceList);
            } catch (Exception ex)
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

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateAttendance([FromBody] AttendanceDto attendance)
        {
            if (attendance == null)
            {
                return BadRequest();
            }
            bool validAttendance = _attendance.GetAll().Any(a => a.EmployeeId == attendance.EmployeeId);
            if (validAttendance)
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Attendance>(attendance);
            _attendance.Add(temp);
            return Ok(temp);
        }

        [Authorize]
        [HttpPut("update/attendance/{employeeId}")]
        public IActionResult UpdateAttendance(string employeeId, [FromBody] UpdateAttendanceDto attendance)
        {
            if (attendance == null)
            {
                return BadRequest();
            }
            var validAttendance = _attendance.GetAll().Where(a => a.EmployeeId == employeeId).FirstOrDefault();
            if (validAttendance == null)
            {
                return BadRequest();
            }
            _mapper.Map(attendance, validAttendance);
            validAttendance.EmployeeId = employeeId;

            _attendance.Update(validAttendance);
            return Ok(validAttendance);
        }

        [SwaggerOperation(Summary = 
            "Create a new attendance for the employee and late hour will be calculated automatically, if note will contain \"Arrived late\"")]
        [HttpGet("punch-in/attendance/{employeeId}")]
        public IActionResult PunchIn(string employeeId)
        {
            try
            {
                //generate new automatically attendanceID
                int count = _attendance.GetAll().Count() + 1;
                var attendanceId = "AT" + count.ToString().PadLeft(6, '0');

                var attendanceList = _attendance.GetAll().Where(a => a.EmployeeId == employeeId);
                if (attendanceList.ToList().Count() <= 0)
                {
                    return BadRequest("Not found employeeId");
                }
                DateTime datePunchIn = DateTime.Now;
                if(attendanceList.Any(a => a.Day == datePunchIn.Date))
                {
                    return BadRequest("Already Attended");
                }
                //get timeIn
                var startOfTheDate = DateTime.Now.Date;
                var timeIn = datePunchIn - startOfTheDate;
                //get lateHour
                DateTime eightAM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var lateHour = datePunchIn - eightAM;
                if(lateHour < TimeSpan.Zero)
                {
                    lateHour = TimeSpan.Zero;
                }
                //get note
                var note = "";
                if (lateHour > TimeSpan.Zero)
                {
                    note = "Arrived late";
                }
                else note = "Regular working hours";
                Attendance punchInInfo = new Attendance()
                {
                    AttendanceId = attendanceId,
                    EmployeeId = employeeId,
                    Day = datePunchIn.Date,
                    TimeIn = timeIn,
                    LateHours = lateHour,
                    AttendanceStatus = false,
                    Notes = note,
                };
                _attendance.Add(punchInInfo);
                return Ok(_mapper.Map<AttendanceDto>(punchInInfo));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("punch-out/attendance/{employeeId}")]
        public IActionResult PunchOut(string employeeId)
        {
            var attendanceList = _attendance.GetAll().Where(a => a.EmployeeId == employeeId);
            if (attendanceList.ToList().Count() <= 0)
            {
                return BadRequest("Not found employeeId");
            }
            DateTime datePunchOut = DateTime.Now;
            if (!attendanceList.Any(a => a.Day == datePunchOut.Date))
            {
                return BadRequest("Have not punched in");
            }
            var empAttendance = attendanceList.Where(a => a.Day == datePunchOut.Date).FirstOrDefault();
            //get timeOut
            var startOfTheDate = DateTime.Now.Date;
            var timeOut = datePunchOut - startOfTheDate;
            //get early leave hour 
            DateTime fivePM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);
            var earlyLeaveHour = fivePM - datePunchOut;
            if(earlyLeaveHour < TimeSpan.Zero)
            {
                earlyLeaveHour = TimeSpan.Zero;
            }
            //get total hour
            var totalHours = timeOut - empAttendance.TimeIn;
            if(totalHours > TimeSpan.FromHours(8)) 
            {
                totalHours = TimeSpan.FromHours(8);
            }
            empAttendance.TimeOut = timeOut;
            empAttendance.EarlyLeaveHours = earlyLeaveHour;
            empAttendance.TotalHours = totalHours;
            empAttendance.AttendanceStatus = true;

            _attendance.Update(empAttendance);
            return Ok(_mapper.Map<AttendanceDto>(empAttendance));
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
