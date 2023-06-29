﻿using AutoMapper;
using HumanResourceApi.DTO.Employee;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly EmployeeRepo _employeeRepo;

        public EmployeeController(IMapper mapper, EmployeeRepo employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        [HttpGet("employees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employeeList = _mapper.Map<List<EmployeeDto>>(_employeeRepo.GetAll());
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get/employee")]
        public IActionResult GetEmployeesById([FromQuery] string id)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepo.GetAll().Where(e => e.EmployeeId == id).FirstOrDefault());
            if (employee == null)
            {
                return BadRequest();
            }
            return Ok(employee);
        }

        [HttpGet("get/user/{userId}/employee")]
        public IActionResult GetEmployeeByUserId(string userId)
        {
            try
            {
                var employee = _mapper.Map<EmployeeDto>(_employeeRepo.GetAll().Where(e => e.UserId == userId && e.Status.Equals("active")).FirstOrDefault());
                if(employee == null)
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
                var employee = _mapper.Map<EmployeeDto>(_employeeRepo.GetAll().Where(e => e.UserId == userId && e.Status.Equals("active")).FirstOrDefault());
                if (employee == null)
                {
                    return NotFound("Employee seems to be null");
                }
                var employeeList = _mapper.Map<List<EmployeeDto>>(_employeeRepo.GetAll().Where(e => e.DepartmentId == employee.DepartmentId && e.Status.Equals("active")).ToList());
                if (employeeList == null)
                {
                    return NotFound("Employee in this department seems to be null");
                }
                return Ok(employeeList);
            }
            catch (Exception ex)
            {

                return BadRequest("Something went wrong: " +  ex.Message);
            }
            
        }
        [HttpPost("create")]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            if (_employeeRepo.GetAll().Any(e => e.EmployeeId == employee.EmployeeId))
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Employee>(employee);
            _employeeRepo.Add(temp);
            return Ok(temp);
        }

        [HttpPut("update")]
        public IActionResult UpdateEmployee([FromQuery] string id, [FromBody] UpdateEmployeeDto employee)
        {
            if (employee == null )
            {
                return BadRequest();
            }
            var validEmployee = _employeeRepo.GetAll().Where(e => e.EmployeeId == id && e.Status.Equals("active")).FirstOrDefault();
            if (validEmployee == null)
            {
                return BadRequest();
            }
            _mapper.Map(employee, validEmployee);
            validEmployee.EmployeeId = id;

            _employeeRepo.Update(validEmployee);
            return Ok(validEmployee);
        }

        [HttpPost("delete")]
        public IActionResult DeleteEmployee([FromQuery] string id)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepo.GetAll().Where(e => e.EmployeeId == id).FirstOrDefault());
            if (employee == null)
            {
                return BadRequest();
            }
            var validEmployee = _employeeRepo.GetAll().Where(e => e.EmployeeId == id).FirstOrDefault();
            _mapper.Map(employee, validEmployee);
            validEmployee.Status = "Disable";

            _employeeRepo.Update(validEmployee);
            return Ok(validEmployee);
        }
    }
}