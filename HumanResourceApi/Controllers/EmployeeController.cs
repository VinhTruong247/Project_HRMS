using AutoMapper;
using HumanResourceApi.DTO.Employee;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Text.RegularExpressions;
using HumanResourceApi.Helper;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeRepo _employeeRepo;
        public readonly UserRepo _userRepo;
        public Regex x = new Regex(@"^EP\d{6}");
        public Regex y = new Regex(@"^US\d{6}");
        public Regex z = new Regex(@"^DP\d{6}");
        public Regex t = new Regex(@"^JB\d{6}");
        public Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");


        public EmployeeController(IMapper mapper, EmployeeRepo employeeRepo, UserRepo userRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _userRepo = userRepo;
        }

        [Authorize]
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
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [HttpGet("get/employee/{employeeId}")]
        public IActionResult GetEmployeesById(string employeeId)
        {
            try
            {
                if (!x.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
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
                if (!y.IsMatch(userId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var employee = _mapper.Map<EmployeeDto>(_userRepo.getEmployee(userId));
                if (employee is null)
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
                if (!y.IsMatch(userId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
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
                if (!x.IsMatch(employee.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!z.IsMatch(employee.DepartmentId))
                {
                    return BadRequest("Wrong departmentId Format.");
                }
                if (!t.IsMatch(employee.JobId))
                {
                    return BadRequest("Wrong jobId Format.");
                }
                if (_employeeRepo.GetAll().Any(e => e.EmployeeId == employee.EmployeeId))
                {
                    return BadRequest("Employee ID = " + employee.EmployeeId + " existed");
                }
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

        [HttpPut("update/profile/{employeeId}")]
        public IActionResult UpdateProfile(string employeeId, [FromBody] UpdateProfileDto updateProfile)
        {
            try
            {
                if (updateProfile == null)
                {
                    return BadRequest("Profile input seems to be null");
                }
                var updateEmployee = _employeeRepo.GetById(employeeId);
                if (updateEmployee == null)
                {
                    return NotFound($"Can't find employee with Id: {employeeId}");
                }
                _mapper.Map(updateProfile, updateEmployee);
                _employeeRepo.Update(updateEmployee);

                var mappedProfile = _mapper.Map<UpdateProfileDto>(updateEmployee);
                return Ok(mappedProfile);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
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
                if (!x.IsMatch(employeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                if (!z.IsMatch(employee.DepartmentId))
                {
                    return BadRequest("Wrong departmentId Format.");
                }
                if (!t.IsMatch(employee.JobId))
                {
                    return BadRequest("Wrong jobId Format.");
                }
                if (!emailRegex.IsMatch(employee.Email))
                {
                    return BadRequest("Invalid Email Format");
                }
                var validEmployee = _employeeRepo.GetAll().Where(e => e.EmployeeId == employeeId).FirstOrDefault();
                if (validEmployee == null)
                {
                    return BadRequest("Employee ID = " + employeeId + " doesn't seem to be found.");
                }
                _mapper.Map(employee, validEmployee);
                validEmployee.EmployeeId = employeeId;

                _employeeRepo.Update(validEmployee);
                var mappedEmployee = _mapper.Map<EmployeeDto>(validEmployee);
                return Ok(mappedEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("employees/search/{keyword}")]
        public IActionResult FindEmployee(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<EmployeeDto>>(_employeeRepo.GetAll().Where(e => RemoveVietnameseSign.RemoveSign(e.FirstName).ToLower().Contains(keyword.ToLower()) ||
                RemoveVietnameseSign.RemoveSign(e.LastName).ToLower().Contains(keyword.ToLower()))
                );
                if (resultList == null)
                {
                    return BadRequest("No active employee(s) found");
                }

                return Ok(resultList.OrderBy(e => e.EmployeeId));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
