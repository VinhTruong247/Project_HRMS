using AutoMapper;
using HumanResourceApi.DTO.Department;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly DepartmentRepo _repo;
        public Regex x = new Regex(@"^DP\d{6}");

        public DepartmentController(IMapper mapper, DepartmentRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [Authorize]
        [HttpGet("departments")]
        public IActionResult GetDepartment()
        {
            try
            {
                var departmentList = _mapper.Map<List<DepartmentDto>>(_repo.GetAll());
                return Ok(departmentList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/department/{departmentId}")]
        public IActionResult GetDepartmentId(string departmentId)
        {
            try
            {
                if (!x.IsMatch(departmentId))
                {
                    return BadRequest("Wrong DepartmentId Format.");
                }
                var department = _mapper.Map<DepartmentDto>(_repo.GetAll().Where(d => d.DepartmentId == departmentId).FirstOrDefault());
                if (department == null)
                {
                    return BadRequest("Department ID = " + departmentId + " doesn't seem to be found");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateDepartment([FromBody] DepartmentDto department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(department.DepartmentId))
                {
                    return BadRequest("Wrong DepartmentId Format.");
                }
                if (_repo.GetAll().Any(d => d.DepartmentId == department.DepartmentId))
                {
                    return BadRequest("Department ID = " + department.DepartmentId + " existed.");
                }
                var temp = _mapper.Map<Department>(department);
                _repo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update/department/{departmentId}")]
        public IActionResult UpdateDepartment(string departmentId, [FromBody] UpdateDepartmentDto department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(departmentId))
                {
                    return BadRequest("Wrong DepartmentId Format.");
                }
                var validDepartment = _repo.GetAll().Where(d => d.DepartmentId == departmentId).FirstOrDefault();
                if (validDepartment == null)
                {
                    return BadRequest("Department ID = " + departmentId + " doesn't seem to be found");
                }
                _mapper.Map(department, validDepartment);
                validDepartment.DepartmentId = departmentId;

                _repo.Update(validDepartment);
                return Ok(validDepartment);
            } catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        //[Authorize]
        //[HttpPost("delete")]
        //public IActionResult DeleteDepartment([FromQuery] string id)
        //{
        //    var department = _mapper.Map<DepartmentDto>(_repo.GetAll().Where(d => d.DepartmentId == id).FirstOrDefault());
        //    if (department == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (department.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validDepartment = _repo.GetAll().Where(d => d.DepartmentId == id).FirstOrDefault();
        //    _mapper.Map(department, validDepartment);
        //    validDepartment.Status = "Disable";
        //    _repo.Update(validDepartment);
        //    return Ok(validDepartment);
        //}
    }
}
