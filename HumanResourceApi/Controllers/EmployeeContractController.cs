using AutoMapper;
using HumanResourceApi.DTO.EmployeeContract;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeContractRepo _employeeContractRepo;

        public EmployeeContractController(IMapper mapper, EmployeeContractRepo employeeContractRepo)
        {
            _mapper = mapper;
            _employeeContractRepo = employeeContractRepo;
        }

        [Authorize]
        [HttpGet("contracts")]
        public IActionResult GetContracts()
        {
            try
            {
                var contractsList = _mapper.Map<List<EmployeeContractDto>>(_employeeContractRepo.GetAll());
                return Ok(contractsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/contract")]
        public IActionResult GetContract([FromQuery] string id)
        {
            var contract = _mapper.Map<EmployeeContractDto>(_employeeContractRepo.GetAll().Where(c => c.ContractId == id));
            if (contract == null)
            {
                return BadRequest();
            }
            return Ok(contract);
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateContract([FromBody] EmployeeContractDto contract)
        {
            if (contract == null)
            {
                return BadRequest();
            }
            bool validContract = _employeeContractRepo.GetAll().Any(c =>  c.ContractId == contract.ContractId);
            if (validContract)
            {
                return BadRequest();
            }
            var temp = _mapper.Map<EmployeeContract>(contract);
            _employeeContractRepo.Add(temp);
            return Ok(temp);
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateContractId([FromQuery] string id, [FromBody] UpdateEmployeeContractDto contract)
        {
            if (contract == null)
            {
                return BadRequest();
            }
            var validContract = _employeeContractRepo.GetAll().Where(c => c.ContractId == id).FirstOrDefault();
            if (validContract == null)
            {
                return BadRequest();
            }
            _mapper.Map(contract, validContract);
            validContract.ContractId = id;

            _employeeContractRepo.Update(validContract);
            return Ok(validContract);
        }

        [Authorize]
        [HttpPost("delete")]
        public IActionResult DeleteContract([FromQuery] string id)
        {
            var contract = _mapper.Map<EmployeeContractDto>(_employeeContractRepo.GetAll().Where(c => c.ContractId == id).FirstOrDefault());
            if (contract == null)
            {
                return BadRequest();
            }
            if (contract.Status == "Disable")
            {
                return BadRequest("ID = " + id + " is already disabled");
            }
            var validContract = _employeeContractRepo.GetAll().Where(c => c.ContractId == id).FirstOrDefault();
            _mapper.Map(contract, validContract);
            validContract.Status = "Disable";

            _employeeContractRepo.Update(validContract);
            return Ok(validContract);
        }
    }
}
