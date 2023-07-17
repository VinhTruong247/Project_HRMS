using AutoMapper;
using HumanResourceApi.DTO.Employee;
using HumanResourceApi.DTO.Project;
using HumanResourceApi.Helper;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ProjectRepo _projectRepo;
        public Regex projectIdRegex = new Regex(@"^PJ\d{6}");
        public Regex departmentIdRegex = new Regex(@"^DP\d{6}");

        public ProjectController(IMapper mapper, ProjectRepo projectRepo)
        {
            _mapper = mapper;
            _projectRepo = projectRepo;
        }

        [HttpGet("get/projects")]
        public IActionResult GetAllProject()
        {
            try
            {
                var projectList = _mapper.Map<List<ProjectDto>>(_projectRepo.GetAll());
                if (projectList == null)
                {
                    return BadRequest("There's no active project");
                }
                return Ok(projectList);
            } catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/project/{projectId}")]
        public IActionResult GetProjectById(string projectId)
        {
            try
            {
                if (!projectIdRegex.IsMatch(projectId))
                {
                    return BadRequest("Wrong projectId Format.");
                }
                var project = _mapper.Map<ProjectDto>(_projectRepo.GetAll().Where(p => p.ProjectId == projectId).FirstOrDefault());
                if (project == null)
                {
                    return BadRequest("Project ID = " + projectId + " doesn't seem to be found.");
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateProject([FromBody] ProjectDto project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!projectIdRegex.IsMatch(project.ProjectId))
                {
                    return BadRequest("Wrong projectId Format.");
                }
                if (!departmentIdRegex.IsMatch(project.DepartmentId))
                {
                    return BadRequest("Wrong departmentId Format.");
                }
                if (_projectRepo.GetAll().Any(p => p.ProjectId == project.ProjectId))
                {
                    return BadRequest("Project ID = " + project.ProjectId + " existed");
                }
                var temp = _mapper.Map<Project>(project);
                _projectRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/project/{projectId}")]
        public IActionResult UpdateProject(string projectId, [FromBody] UpdateProjectDto project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!projectIdRegex.IsMatch(projectId))
                {
                    return BadRequest("Wrong projectId Format.");
                }
                if (!departmentIdRegex.IsMatch(project.DepartmentId))
                {
                    return BadRequest("Wrong departmentId Format.");
                }
                var valid = _projectRepo.GetAll().Where(p => p.ProjectId == projectId).FirstOrDefault();
                if (valid == null)
                {
                    return BadRequest("Project ID = " + projectId + " doesn't seem to be existed");
                }
                _mapper.Map(project, valid);
                valid.ProjectId = projectId;

                _projectRepo.Update(valid);
                return Ok(valid);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("projects/search/{keyword}")]
        public IActionResult FindProject(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<ProjectDto>>(_projectRepo.GetAll().Where(e => RemoveVietnameseSign.RemoveSign(e.ProjectName).ToLower().Contains(keyword.ToLower())));
                if (resultList == null)
                {
                    return BadRequest("No active project(s) found");
                }
                return Ok(resultList.OrderBy(p => p.ProjectId));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
