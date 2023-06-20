using AutoMapper;
using HumanResourceApi.Models;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ExperienceRepo _experienceRepo;

        public ExperienceController(IMapper mapper, ExperienceRepo experienceRepo)
        {
            _mapper = mapper;
            _experienceRepo = experienceRepo;
        }

        [HttpGet("experiences")]
        public IActionResult GetExperiences()
        {
            try
            {
                var experienceList = _mapper.Map<List<ExperienceDto>>(_experienceRepo.GetAll());

                // Return the list of users
                return Ok(experienceList);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get/experience/{id}")]
        public IActionResult getExperienceId(int id)
        {
            var experience = _mapper.Map<ExperienceDto>(_experienceRepo.GetAll().Where(e => e.ExperienceId == id).FirstOrDefault());

            if (experience == null)
            {
                return BadRequest();
            }
            return Ok(experience);
        }

        [HttpPost("create")]
        public IActionResult CreateExp([FromBody] ExperienceDto experience)
        {
            bool validExp = _experienceRepo.GetAll().Any(e => e.ExperienceId == experience.ExperienceId);
            if (validExp)
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Experience>(experience);
            _experienceRepo.Add(temp);
            return Ok(temp);
        }

        [HttpPost("update")]
        public IActionResult UpdateExp([FromQuery] int id, [FromBody] UpdateExperienceDto experience)
        {
            if (experience == null)
                return BadRequest();
            var validExp = _experienceRepo.GetAll().Where(e => e.ExperienceId == id).FirstOrDefault();
            if (validExp == null)
            {
                return BadRequest();
            }
            _mapper.Map(experience, validExp);
            validExp.ExperienceId = id;

            _experienceRepo.Update(validExp);
            return Ok(validExp);
        }


        //                              MUST DO (LATER)
        //[HttpPost("del/experience")]
        //public IActionResult DisableExperienceId(int id)
        //{
        //    var experience = _mapper.Map<ExperienceDto>(_experienceRepo.GetAll().Where(e => e.ExperienceId == id).FirstOrDefault());

        //    if (experience == null)
        //    {
        //        return BadRequest();
        //    }
        //    experience.
        //}
    }
}
