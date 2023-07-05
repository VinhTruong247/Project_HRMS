using AutoMapper;
using HumanResourceApi.DTO.Deduction;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly DeductionRepo _deductionRepo;
        public Regex x = new Regex(@"^DD\d{6}");

        public DeductionController(IMapper mapper, DeductionRepo deductionRepo)
        {
            _mapper = mapper;
            _deductionRepo = deductionRepo;
        }

        [HttpGet("deductions")]
        public IActionResult GetAllDeductions()
        {
            try
            {
                var deductionList = _mapper.Map<List<DeductionDto>>(_deductionRepo.GetAll());
                return Ok(deductionList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/deduction/{deductionId}")]
        public IActionResult GetDeductionById(string deductionId)
        {
            try
            {
                if (!x.IsMatch(deductionId))
                {
                    return BadRequest("Wrong DeductionId Format.");
                }
                var deduction = _mapper.Map<DeductionDto>(_deductionRepo.GetAll().Where(d => d.DeductionId == deductionId).FirstOrDefault());
                if (deduction == null)
                {
                    return BadRequest("Deduction ID = " + deductionId + " doesn't seem to be found");
                }
                return Ok(deduction);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateDeduction([FromBody] DeductionDto deduction)
        {
            try
            {
                if (deduction == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(deduction.DeductionId))
                {
                    return BadRequest("Wrong DeductionId Format.");
                }
                if (_deductionRepo.GetAll().Any(d => d.DeductionId == deduction.DeductionId))
                {
                    return BadRequest("Deduction ID = " + deduction.DeductionId + " existed.");
                }
                var temp = _mapper.Map<Deduction>(deduction);
                _deductionRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/deduction/{deductionId}")]
        public IActionResult UpdateDeduction(string deductionId, [FromBody] UpdateDeductionDto deduction)
        {
            try
            {
                if (deduction == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(deductionId))
                {
                    return BadRequest("Wrong DeductionId Format.");
                }
                var validDeduction = _deductionRepo.GetAll().Where(d => d.DeductionId == deductionId).FirstOrDefault();
                if (validDeduction == null)
                {
                    return BadRequest("Deduction ID = " + deductionId + " doesn't seem to be found");
                }
                _mapper.Map(deduction, validDeduction);
                validDeduction.DeductionId = deductionId;

                _deductionRepo.Update(validDeduction);
                return Ok(validDeduction);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        //[HttpPost("delete")]
        //public IActionResult DeleteDeduction([FromQuery] string id)
        //{
        //    var deduction = _mapper.Map<DeductionDto>(_deductionRepo.GetAll().Where(d => d.DeductionId == id).FirstOrDefault());
        //    if (deduction == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (deduction.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validDeduction = _deductionRepo.GetAll().Where(d => d.DeductionId == id).FirstOrDefault();
        //    _mapper.Map(deduction, validDeduction);
        //    validDeduction.Status = "Disable";

        //    _deductionRepo.Update(validDeduction);
        //    return Ok(validDeduction);
        //}
    }
}
