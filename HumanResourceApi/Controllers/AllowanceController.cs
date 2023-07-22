using AutoMapper;
using HumanResourceApi.DTO.Allowance;
using HumanResourceApi.DTO.Department;
using HumanResourceApi.Helper;
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
                int count = _allowance.GetAll().Count() + 1;
                var allowanceId = "AL" + count.ToString().PadLeft(6, '0');
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

        [HttpGet("allowance/search/{keyword}")]
        public IActionResult FindProject(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<ResponseAllowanceDto>>(_allowance.GetAll().Where(a => RemoveVietnameseSign.RemoveSign(a.AllowanceType).ToLower().Contains(keyword.ToLower())));
                if (resultList == null)
                {
                    return BadRequest("No active allowance(s) found");
                }
                return Ok(resultList.OrderBy(e => e.AllowanceId));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}