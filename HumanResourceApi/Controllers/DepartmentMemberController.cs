﻿using AutoMapper;
using HumanResourceApi.DTO.DepartmentMemberList;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentMemberController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly DepartmentMemberRepo _departmentMemberRepo;
        public Regex x = new Regex(@"^DP\d{6}");
        public Regex y = new Regex(@"^EP\d{6}");

        public DepartmentMemberController(IMapper mapper, DepartmentMemberRepo departmentMemberRepo)
        {
            _mapper = mapper;
            _departmentMemberRepo = departmentMemberRepo;
        }

        [HttpGet("get/DepartmentMemberList")]
        public IActionResult GetList()
        {
            try
            {
                var list = _mapper.Map<List<DepartmentMemberDto>>(_departmentMemberRepo.GetAll());
                if (list == null)
                {
                    return BadRequest("There's no active department");
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/DepartmentMemberList/{departmentId}")]
        public IActionResult GetById(string departmentId)
        {
            try
            {
                if (!x.IsMatch(departmentId))
                {
                    return BadRequest("Wrong DepartmentId Format.");
                }
                var result = _mapper.Map<DepartmentMemberDto>(_departmentMemberRepo.GetAll().Where(a => a.DepartmentId == departmentId).FirstOrDefault());
                if (result == null)
                {
                    return BadRequest("Department ID = " + departmentId + " doesn't seem to be found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DepartmentMemberDto departmentMember)
        {
            try
            {
                if (departmentMember == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(departmentMember.DepartmentId))
                {
                    return BadRequest("Wrong DepartmentId Format.");
                }
                if (!y.IsMatch(departmentMember.EmployeeId))
                {
                    return BadRequest("Wrong EmployeeId Format.");
                }
                if (_departmentMemberRepo.GetAll().Any(a => a.DepartmentId == departmentMember.DepartmentId))
                {
                    return BadRequest("Department ID = " + departmentMember.DepartmentId + " 's member list existed");
                }
                var temp = _mapper.Map<DepartmentMemberList>(departmentMember);
                _departmentMemberRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }

        }

        [HttpPut("update/DepartmentMemberList/{departmentId}")]
        public IActionResult UpdateMemberList(string departmentId, [FromBody] UpdateDepartmentMemberDto departmentMember)
        {
            try
            {
                if (departmentMember == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!x.IsMatch(departmentId))
                {
                    return BadRequest("Wrong DepartmentId Format.");
                }
                if (!y.IsMatch(departmentMember.EmployeeId))
                {
                    return BadRequest("Wrong EmployeeId Format.");
                }
                var valid = _departmentMemberRepo.GetAll().Where(a => a.DepartmentId == departmentId).FirstOrDefault();
                if (valid == null)
                {
                    return BadRequest("Department ID = " + departmentId + " doesn't seem to exist.");
                }
                _mapper.Map(departmentMember, valid);
                valid.DepartmentId = departmentId;

                _departmentMemberRepo.Update(valid);
                return Ok(valid);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
