using AutoMapper;
using HumanResourceApi.DTO.Attendance;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpGet("attendances")]
        public IActionResult GetAttendance()
        {
            try
            {
                var attendanceList = _mapper.Map<List<AttendanceDto>>(_attendance.GetAll());
                return Ok(attendanceList);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/attendance")]
        public IActionResult GetAttendanceId([FromBody] string id)
        {
            var attendance = _mapper.Map<AttendanceDto>(_attendance.GetAll().Where(a => a.EmployeeId == id).FirstOrDefault());
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
        [HttpPut("update")]
        public IActionResult UpdateAttendance([FromQuery] string id, [FromBody] UpdateAttendanceDto attendance)
        {
            if (attendance == null)
            {
                return BadRequest();
            }
            var validAttendance = _attendance.GetAll().Where(a => a.EmployeeId == id).FirstOrDefault();
            if (validAttendance == null)
            {
                return BadRequest();
            }
            _mapper.Map(attendance, validAttendance);
            validAttendance.EmployeeId = id;

            _attendance.Update(validAttendance);
            return Ok(validAttendance);
        }

        [Authorize]
        [HttpPost("delete")]
        public IActionResult DeleteAttendance([FromQuery] string id)
        {
            var attendance = _mapper.Map<AttendanceDto>(_attendance.GetAll().Where(a => a.EmployeeId == id).FirstOrDefault());
            if (attendance == null)
            {
                return BadRequest();
            }
            if (attendance.AttendanceStatus == "Disable")
            {
                return BadRequest("ID = " + id + " is already disabled");
            }
            var validAttendance = _attendance.GetAll().Where(a => a.EmployeeId == id).FirstOrDefault();
            _mapper.Map(attendance, validAttendance);
            validAttendance.AttendanceStatus = "Disable";

            _attendance.Update(validAttendance);
            return Ok(validAttendance);
        }
    }
}
