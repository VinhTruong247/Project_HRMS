using AutoMapper;
using HumanResourceApi.DTO.DeductionSummary;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionSumaryController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly DeductionSumaryRepo _deductionSumaryRepo;
        public Regex x = new Regex(@"^DD\d{6}");

        public DeductionSumaryController(IMapper mapper, DeductionSumaryRepo deductionSumaryRepo)
        {
            _mapper = mapper;
            _deductionSumaryRepo = deductionSumaryRepo;
        }

        [HttpGet("deductionSumaries")]
        public IActionResult GetList()
        {
            try
            {
                var deductionSumaryList = _mapper.Map<List<DeductionSumaryDto>>(_deductionSumaryRepo.GetAll());
                return Ok(deductionSumaryList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/deductionSumary/{deductionId}")]
        public IActionResult GetUsingId(string deductionId)
        {
            try
            {
                if (!x.IsMatch(deductionId))
                {
                    return BadRequest("Wrong DeductionId Format.");
                }
                var deductionSumary = _mapper.Map<DeductionSumaryDto>(_deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == deductionId).FirstOrDefault());
                if (deductionSumary == null)
                {
                    return BadRequest("Deduction ID = " + deductionId + " doesn't seem to be found");
                }
                return Ok(deductionSumary);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [HttpPost("create")]
        public IActionResult CreateDeductionSumary([FromBody] DeductionSumaryDto deductionSumary)
        {
            try
            {
                if (deductionSumary == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(deductionSumary.DeductionId))
                {
                    return BadRequest("Wrong DeductionId Format.");
                }
                if (_deductionSumaryRepo.GetAll().Any(ds => ds.DeductionId == deductionSumary.DeductionId))
                {
                    return BadRequest("Deduction ID = " + deductionSumary.DeductionId + " existed");
                }
                var temp = _mapper.Map<DeductionSumary>(deductionSumary);
                _deductionSumaryRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [HttpPut("update/deuctionSumary/{deductionId}")]
        public IActionResult UpdateDeductionSumary(string deductionId, [FromBody] UpdateDeductionSumaryDto deductionSumary)
        {
            try
            {
                if (deductionSumary == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(deductionId))
                {
                    return BadRequest("Wrong DeductionId Format.");
                }
                var validDS = _deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == deductionId).FirstOrDefault();
                if (validDS == null)
                {
                    return BadRequest("Deduction ID = " + deductionId + " doesn't seem to be found");
                }
                _mapper.Map(deductionSumary, validDS);
                validDS.DeductionId = deductionId;

                _deductionSumaryRepo.Update(validDS);
                return Ok(validDS);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        //[HttpPost("delete")]
        //public IActionResult DeleteDeductionSumary([FromQuery] string id)
        //{
        //    var deductionSumary = _mapper.Map<DeductionSumaryDto>(_deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == id).FirstOrDefault());
        //    if (deductionSumary == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (deductionSumary.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validDS = _deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == id).FirstOrDefault();
        //    _mapper.Map(deductionSumary, validDS);
        //    validDS.Status = "Disable";

        //    _deductionSumaryRepo.Update(validDS);
        //    return Ok(validDS);
        //}
    }
}
