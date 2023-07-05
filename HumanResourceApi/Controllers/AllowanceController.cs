using AutoMapper;
using HumanResourceApi.DTO.Allowance;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllowanceController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly AllowanceRepo _allowance;
        public Regex x = new Regex(@"^AL\d{6}");

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
        [HttpGet("get/allowance/{allowanceId}")]
        public IActionResult GetAllowanceId(string allowanceId)
        {
            try
            {
                if (!x.IsMatch(allowanceId))
                {
                    return BadRequest("Wrong AllowanceId Format.");
                }
                var allowance = _mapper.Map<AllowanceDto>(_allowance.GetAll().Where(a => a.AllowanceId == allowanceId).FirstOrDefault());
                if (allowance == null)
                {
                    return BadRequest("Allowance ID = " + allowanceId + " doesn't seem to be found");
                }
                return Ok(allowance);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateAllowance([FromBody] AllowanceDto allowance)
        {
            try
            {
                if (allowance == null)
                {
                    return BadRequest();
                }
                if (!x.IsMatch(allowance.AllowanceId))
                {
                    return BadRequest("Wrong AllowanceId Format.");
                }
                bool validAllowance = _allowance.GetAll().Any(a => a.AllowanceId == allowance.AllowanceId);
                if (validAllowance)
                {
                    return BadRequest("Allowance ID = " + allowance.AllowanceId + " existed");
                }
                var temp = _mapper.Map<Allowance>(allowance);
                _allowance.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update/allowance/{allowanceId}")]
        public IActionResult UpdateAllowanceId(string allowanceId, [FromBody] UpdateAllowanceDto allowance)
        {
            try
            {
                if (allowance == null)
                {
                    return BadRequest();
                }
                if (!x.IsMatch(allowanceId))
                {
                    return BadRequest("Wrong AllowanceId Format.");
                }
                var validAllowance = _allowance.GetAll().Where(a => a.AllowanceId == allowanceId).FirstOrDefault();
                if (validAllowance == null)
                {
                    return BadRequest("Allowance ID = " + allowanceId + " doesn't seem to be found");
                }
                _mapper.Map(allowance, validAllowance);
                validAllowance.AllowanceId = allowanceId;

                _allowance.Update(validAllowance);
                return Ok(validAllowance);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        //[Authorize]
        //[HttpPost("delete")]
        //public IActionResult DeleteAllowance([FromQuery] string id)
        //{
        //    var allowance = _mapper.Map<AllowanceDto>(_allowance.GetAll().Where(a => a.AllowanceId == id).FirstOrDefault());
        //    if (allowance == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (allowance.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validAllowance = _allowance.GetAll().Where(a => a.AllowanceId == id).FirstOrDefault();
        //    _mapper.Map(allowance, validAllowance);
        //    validAllowance.Status = "Disable";

        //    _allowance.Update(validAllowance);
        //    return Ok(validAllowance);
        //}
    }
}