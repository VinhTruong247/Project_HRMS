using AutoMapper;
using HumanResourceApi.DTO.Leave;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LeaveRepo _leaveRepo;

        public LeaveController(IMapper mapper, LeaveRepo leaveRepo)
        {
            _mapper = mapper;
            _leaveRepo = leaveRepo;
        }

        [HttpGet("leaves")]
        public IActionResult GetLeave()
        {
            try
            {
                var leaveList = _mapper.Map<List<LeaveDto>>(_leaveRepo.GetAll());
                return Ok(leaveList);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get/leave/{id}")]
        public IActionResult GetLeaveId(string id)
        {
            var leave = _mapper.Map<LeaveDto>(_leaveRepo.GetAll().Where(l => l.LeaveId == id).FirstOrDefault());
            if (leave == null)
            {
                return BadRequest();
            }
            return Ok(leave);
        }

        [HttpPost("create")]
        public IActionResult CreateLeave([FromBody] LeaveDto leave)
        {
            bool validLeave = _leaveRepo.GetAll().Any(l => l.LeaveId == leave.LeaveId);
            if (validLeave)
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Leave>(leave);
            _leaveRepo.Add(temp);
            return Ok(temp);
        }

        [HttpPost("update")]
        public IActionResult UpdateLeave([FromQuery] string id, [FromBody] UpdateLeaveDto leave)
        {
            if (leave == null)
            {
                return BadRequest();
            }
            var validLeave = _leaveRepo.GetAll().Where(l => l.LeaveId == id).FirstOrDefault();
            if (validLeave == null)
            {
                return BadRequest();
            }
            _mapper.Map(leave, validLeave);
            validLeave.LeaveId = id;

            _leaveRepo.Update(validLeave);
            return Ok(validLeave);
        }

        [HttpPost("delete/leave/{id}")]
        public IActionResult DeleteLeave(string id)
        {
            var leave = _mapper.Map<LeaveDto>(_leaveRepo.GetAll().Where(l => l.LeaveId == id).FirstOrDefault());
            if (leave == null)
            {
                return BadRequest();
            }
            var validLeave = _leaveRepo.GetAll().Where(l => l.LeaveId == id).FirstOrDefault();
            _mapper.Map(leave, validLeave);
            validLeave.LeaveId = id;
            validLeave.Status = "disabled";

            _leaveRepo.Update(validLeave);
            return Ok(validLeave);
        }
    }
}
