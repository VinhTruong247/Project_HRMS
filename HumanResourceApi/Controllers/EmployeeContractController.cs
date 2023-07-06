using AutoMapper;
using HumanResourceApi.DTO.EmployeeContract;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContractController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeContractRepo _employeeContractRepo;
        public Regex contractIdRegex = new Regex(@"^CT\d{6}");
        public Regex employeeIdRegex = new Regex(@"^CT\d{6}");


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
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/contract/{contractId}")]
        public IActionResult GetContract(string contractId)
        {
            try
            {
                if (!contractIdRegex.IsMatch(contractId))
                {
                    return BadRequest("Wrong contractId Format.");
                }
                var contract = _mapper.Map<EmployeeContractDto>(_employeeContractRepo.GetAll().Where(c => c.ContractId == contractId));
                if (contract == null)
                {
                    return BadRequest("Contract ID = " + contractId + " doesn't seem to be found.");
                }
                return Ok(contract);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateContract([FromBody] EmployeeContractDto contract)
        {
            try
            {
                if (contract == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!contractIdRegex.IsMatch(contract.ContractId))
                {
                    return BadRequest("Wrong contractId Format.");
                }
                if (!employeeIdRegex.IsMatch(contract.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                bool validContract = _employeeContractRepo.GetAll().Any(c => c.ContractId == contract.ContractId);
                if (validContract)
                {
                    return BadRequest("Contract ID = " + contract.ContractId + " existed");
                }
                var temp = _mapper.Map<EmployeeContract>(contract);
                _employeeContractRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update/contract/{contractId}")]
        public IActionResult UpdateContractId(string contractId, [FromBody] UpdateEmployeeContractDto contract)
        {
            try
            {
                if (contract == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!contractIdRegex.IsMatch(contractId))
                {
                    return BadRequest("Wrong contractId Format.");
                }
                if (!employeeIdRegex.IsMatch(contract.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var validContract = _employeeContractRepo.GetAll().Where(c => c.ContractId == contractId).FirstOrDefault();
                if (validContract == null)
                {
                    return BadRequest("Contract ID = " + contractId + " doesn't seem to be found.");
                }
                _mapper.Map(contract, validContract);
                validContract.ContractId = contractId;

                _employeeContractRepo.Update(validContract);
                return Ok(validContract);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        //[Authorize]
        //[HttpPost("delete")]
        //public IActionResult DeleteContract([FromQuery] string id)
        //{
        //    var contract = _mapper.Map<EmployeeContractDto>(_employeeContractRepo.GetAll().Where(c => c.ContractId == id).FirstOrDefault());
        //    if (contract == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (contract.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validContract = _employeeContractRepo.GetAll().Where(c => c.ContractId == id).FirstOrDefault();
        //    _mapper.Map(contract, validContract);
        //    validContract.Status = "Disable";

        //    _employeeContractRepo.Update(validContract);
        //    return Ok(validContract);
        //}
    }
}
