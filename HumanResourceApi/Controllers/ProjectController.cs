using AutoMapper;
using HumanResourceApi.DTO.Project;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly ProjectRepo _projectRepo;

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
    }
}
