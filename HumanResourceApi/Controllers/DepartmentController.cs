using AutoMapper;
using HumanResourceApi.DTO.Department;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.Helper;
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
                int count = _repo.GetAll().Count() + 1;
                var departmentId = "RP" + count.ToString().PadLeft(6, '0');
                var temp = _mapper.Map<Department>(department);
                temp.DepartmentId = departmentId;
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

        [HttpGet("department/search/{keyword}")]
        public IActionResult FindProject(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<DepartmentDto>>(_repo.GetAll().Where(dp => RemoveVietnameseSign.RemoveSign(dp.DepartmentName).ToLower().Contains(keyword.ToLower())));
                if (resultList == null)
                {
                    return BadRequest("No active department(s) found");
                }
                return Ok(resultList.OrderBy(dp => dp.DepartmentId));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
