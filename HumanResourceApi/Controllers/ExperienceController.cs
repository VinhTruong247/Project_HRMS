using AutoMapper;
using HumanResourceApi.Models;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using HumanResourceApi.DTO.Project;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ExperienceRepo _experienceRepo;
        public Regex experienceIdRegex = new Regex(@"^EX\d{6}");
        public Regex employeeIdRegex = new Regex(@"^EP\d{6}");

        public ExperienceController(IMapper mapper, ExperienceRepo experienceRepo)
        {
            _mapper = mapper;
            _experienceRepo = experienceRepo;
        }

        [Authorize]
        [HttpGet("experiences")]
        public IActionResult GetExperiences()
        {
            try
            {
                var experienceList = _mapper.Map<List<ExperienceDto>>(_experienceRepo.GetAll());

                // Return the list of users
                return Ok(experienceList);

            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/experience/{experienceId}")]
        public IActionResult getExperienceId(string experienceId)
        {
            try
            {
                if (!experienceIdRegex.IsMatch(experienceId))
                {
                    return BadRequest("Wrong experienceId Format.");
                }
                var experience = _mapper.Map<ExperienceDto>(_experienceRepo.GetAll().Where(e => e.ExperienceId == experienceId).FirstOrDefault());

                if (experience == null)
                {
                    return BadRequest("Experience ID = " + experienceId + " doesn't seem to be found.");

                }
                return Ok(experience);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateExp([FromBody] ExperienceDto experience)
        {
            try
            {
                if (experience == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!experienceIdRegex.IsMatch(experience.ExperienceId))
                {
                    return BadRequest("Wrong experienceId Format.");
                }
                if (!employeeIdRegex.IsMatch(experience.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                bool validExp = _experienceRepo.GetAll().Any(e => e.ExperienceId == experience.ExperienceId);
                if (validExp)
                {
                    return BadRequest("Experience ID = " + experience.ExperienceId + " existed");
                }
                var temp = _mapper.Map<Experience>(experience);
                _experienceRepo.Add(temp);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update/experience/{experienceId}")]
        public IActionResult UpdateExp(string experienceId, [FromBody] UpdateExperienceDto experience)
        {
            try
            {
                if (experience == null)
                    return BadRequest();
                if (!experienceIdRegex.IsMatch(experienceId))
                {
                    return BadRequest("Wrong experienceId Format.");
                }
                if (!employeeIdRegex.IsMatch(experience.EmployeeId))
                {
                    return BadRequest("Wrong employeeId Format.");
                }
                var validExp = _experienceRepo.GetAll().Where(e => e.ExperienceId == experienceId).FirstOrDefault();
                if (validExp == null)
                {
                    return BadRequest("Experience ID = " + experienceId + " doesn't seem to be found.");
                }
                _mapper.Map(experience, validExp);
                validExp.ExperienceId = experienceId;

                _experienceRepo.Update(validExp);
                return Ok(validExp);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("experience/search/{keyword}")]
        public IActionResult FindProject(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<ExperienceDto>>(_experienceRepo.GetAll().Where(exp => exp.NameProject.Contains(keyword)));
                if (resultList == null)
                {
                    return BadRequest("No active experience(s) found");
                }
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}
