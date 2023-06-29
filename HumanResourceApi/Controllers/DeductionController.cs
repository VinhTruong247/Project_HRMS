using AutoMapper;
using HumanResourceApi.DTO.Deduction;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly DeductionRepo _deductionRepo;

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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/deduction")]
        public IActionResult GetDeductionById([FromQuery] string id)
        {
            var deduction = _mapper.Map<DeductionDto>(_deductionRepo.GetAll().Where(d => d.DeductionId == id).FirstOrDefault());
            if (deduction == null)
            {
                return BadRequest();
            }
            return Ok(deduction);
        }

        [HttpPost("create")]
        public IActionResult CreateDeduction([FromBody] DeductionDto deduction)
        {
            if (deduction == null)
            {
                return BadRequest();
            }
            if (_deductionRepo.GetAll().Any(d => d.DeductionId == deduction.DeductionId))
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Deduction>(deduction);
            _deductionRepo.Add(temp);
            return Ok(temp);
        }

        [HttpPut("update")]
        public IActionResult UpdateDeduction([FromQuery] string id, [FromBody] UpdateDeductionDto deduction)
        {
            if (deduction == null)
            {
                return BadRequest();
            }
            var validDeduction = _deductionRepo.GetAll().Where(d => d.DeductionId == id).FirstOrDefault();
            if (validDeduction == null)
            {
                return BadRequest();
            }
            _mapper.Map(deduction, validDeduction);
            validDeduction.DeductionId = id;

            _deductionRepo.Update(validDeduction);
            return Ok(validDeduction);
        }

        [HttpPost("delete")]
        public IActionResult DeleteDeduction([FromQuery] string id)
        {
            var deduction = _mapper.Map<DeductionDto>(_deductionRepo.GetAll().Where(d => d.DeductionId == id).FirstOrDefault());
            if (deduction == null)
            {
                return BadRequest();
            }
            if (deduction.Status == "Disable")
            {
                return BadRequest("ID = " + id + " is already disabled");
            }
            var validDeduction = _deductionRepo.GetAll().Where(d => d.DeductionId == id).FirstOrDefault();
            _mapper.Map(deduction, validDeduction);
            validDeduction.Status = "Disable";

            _deductionRepo.Update(validDeduction);
            return Ok(validDeduction);
        }
    }
}
