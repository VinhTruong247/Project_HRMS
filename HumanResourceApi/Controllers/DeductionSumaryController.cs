using AutoMapper;
using HumanResourceApi.DTO.DeductionSummary;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionSumaryController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly DeductionSumaryRepo _deductionSumaryRepo;

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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/deductionSumary")]
        public IActionResult GetUsingId([FromQuery] string id)
        {
            var deductionSumary = _mapper.Map<DeductionSumaryDto>(_deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == id).FirstOrDefault());
            if (deductionSumary == null)
            {
                return BadRequest();
            }
            return Ok(deductionSumary);
        }

        [HttpPost("create")]
        public IActionResult CreateDeductionSumary([FromBody] DeductionSumaryDto deductionSumary)
        {
            if (deductionSumary == null)
            {
                return BadRequest();
            }
            if (_deductionSumaryRepo.GetAll().Any(ds => ds.DeductionId == deductionSumary.DeductionId))
            {
                return BadRequest();
            }
            var temp = _mapper.Map<DeductionSumary>(deductionSumary);
            _deductionSumaryRepo.Add(temp);
            return Ok(temp);
        }

        [HttpPut("update")]
        public IActionResult UpdateDeductionSumary([FromQuery] string id, [FromBody] UpdateDeductionSumaryDto deductionSumary)
        {
            if (deductionSumary == null)
            {
                return BadRequest();
            }
            var validDS = _deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == id).FirstOrDefault();
            if (validDS == null)
            {
                return BadRequest();
            }
            _mapper.Map(deductionSumary, validDS);
            validDS.DeductionId = id;

            _deductionSumaryRepo.Update(validDS);
            return Ok(validDS);
        }

        [HttpPost("delete")]
        public IActionResult DeleteDeductionSumary([FromQuery] string id)
        {
            var deductionSumary = _mapper.Map<DeductionSumaryDto>(_deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == id).FirstOrDefault());
            if (deductionSumary == null)
            {
                return BadRequest();
            }
            if (deductionSumary.Status == "Disable")
            {
                return BadRequest("ID = " + id + " is already disabled");
            }
            var validDS = _deductionSumaryRepo.GetAll().Where(ds => ds.DeductionId == id).FirstOrDefault();
            _mapper.Map(deductionSumary, validDS);
            validDS.Status = "Disable";

            _deductionSumaryRepo.Update(validDS);
            return Ok(validDS);
        }
    }
}
