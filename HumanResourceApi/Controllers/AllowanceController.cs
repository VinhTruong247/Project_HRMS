using AutoMapper;
using HumanResourceApi.DTO.Allowance;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowanceController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly AllowanceRepo _allowance;

        public AllowanceController(IMapper mapper, AllowanceRepo allowance)
        {
            _mapper = mapper;
            _allowance = allowance;
        }

        [Authorize]
        [HttpGet("allowances")]
        public IActionResult GetAllowance()
        {
            try
            {
                var allowanceList = _mapper.Map<List<AllowanceDto>>(_allowance.GetAll());
                
                return Ok(allowanceList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/allowance")]
        public IActionResult GetAllowanceId([FromQuery] string id)
        {
            var allowance = _mapper.Map<AllowanceDto>(_allowance.GetAll().Where(a => a.AllowanceId == id).FirstOrDefault());
            if (allowance == null)
            {
                return BadRequest();
            }
            return Ok(allowance);
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateAllowance([FromBody] AllowanceDto allowance)
        {
            if (allowance == null)
            {
                return BadRequest();
            }
            bool validAllowance = _allowance.GetAll().Any(a => a.AllowanceId == allowance.AllowanceId);
            if (validAllowance)
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Allowance>(allowance);
            _allowance.Add(temp);
            return Ok(temp);
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult UpdateAllowanceId([FromQuery] string id, [FromBody] UpdateAllowanceDto allowance)
        {
            if (allowance == null)
            {
                return BadRequest();
            }
            var validAllowance = _allowance.GetAll().Where(a => a.AllowanceId == id).FirstOrDefault();
            if (validAllowance == null)
            {
                return BadRequest();
            }
            _mapper.Map(allowance, validAllowance);
            validAllowance.AllowanceId = id;

            _allowance.Update(validAllowance);
            return Ok(validAllowance);
        }

        [Authorize]
        [HttpPost("delete")]
        public IActionResult DeleteAllowance([FromQuery] string id)
        {
            var allowance = _mapper.Map<AllowanceDto>(_allowance.GetAll().Where(a => a.AllowanceId == id).FirstOrDefault());
            if (allowance == null)
            {
                return BadRequest();
            }
            if (allowance.Status == "Disable")
            {
                return BadRequest("ID = " + id + " is already disabled");
            }
            var validAllowance = _allowance.GetAll().Where(a => a.AllowanceId == id).FirstOrDefault();
            _mapper.Map(allowance, validAllowance);
            validAllowance.Status = "Disable";

            _allowance.Update(validAllowance);
            return Ok(validAllowance);
        }
    }
}