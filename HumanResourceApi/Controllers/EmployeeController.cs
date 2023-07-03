using AutoMapper;
using HumanResourceApi.DTO.Employee;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeRepo _employeeRepo;
        public readonly UserRepo _userRepo;

        public EmployeeController(IMapper mapper, EmployeeRepo employeeRepo, UserRepo userRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _userRepo = userRepo;
        }

        [HttpGet("employees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employeeList = _mapper.Map<List<EmployeeDto>>(_employeeRepo.GetAll());
                if (employeeList == null)
                {
                    return BadRequest("There's no active employee");
                }
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get/employee/{employeeId}")]
        public IActionResult GetEmployeesById(string employeeId)
        {
            try
            {
                var employee = _mapper.Map<EmployeeDto>(_employeeRepo.GetAll().Where(e => e.EmployeeId == employeeId).FirstOrDefault());
                if (employee == null)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found.");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/user/{userId}/employee")]
        public IActionResult GetEmployeeByUserId(string userId)
        {
            try
            {
                var employee = _mapper.Map<EmployeeDto>(_userRepo.getEmployee(userId));
                if (employee == null)
                {
                    return NotFound("Employee seems to be null");
                }
                return Ok(employee);

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/user/{userId}/department/employee")]
        public IActionResult GetEmployeesFromADepartmentByUserId(string userId)
        {
            try
            {
                var employee = _mapper.Map<EmployeeDto>(_userRepo.getEmployee(userId));
                if (employee == null)
                {
                    return NotFound("Employee seems to be null");
                }
                var employeeList = _mapper.Map<List<EmployeeDto>>(_employeeRepo.GetAll().Where(e => e.DepartmentId == employee.DepartmentId).ToList());
                if (employeeList == null)
                {
                    return NotFound("Employee in this department seems to be null");
                }
                return Ok(employeeList);
            }
            catch (Exception ex)
            {

                return BadRequest("Something went wrong: " + ex.Message);
            }

        }
        [HttpPost("create")]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (_employeeRepo.GetAll().Any(e => e.EmployeeId == employee.EmployeeId))
                {
                    return BadRequest("EmployeeId existed");
                }
                Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!emailRegex.IsMatch(employee.Email))
                {
                    return BadRequest("Invalid Email Format");
                }
                var temp = _mapper.Map<Employee>(employee);
                _employeeRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [HttpPut("update/user/{employeeId}")]
        public IActionResult UpdateEmployee(string employeeId, [FromBody] UpdateEmployeeDto employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Some input information is null");
                }
                Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!emailRegex.IsMatch(employee.Email))
                {
                    return BadRequest("Invalid Email Format");
                }
                var validEmployee = _employeeRepo.GetAll().Where(e => e.EmployeeId == employeeId).FirstOrDefault();
                if (validEmployee == null)
                {
                    return BadRequest();
                }
                _mapper.Map(employee, validEmployee);
                validEmployee.EmployeeId = employeeId;

                _employeeRepo.Update(validEmployee);
                return Ok(validEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
