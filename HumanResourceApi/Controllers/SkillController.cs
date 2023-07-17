using AutoMapper;
using HumanResourceApi.DTO.Project;
using HumanResourceApi.DTO.Skill;
using HumanResourceApi.Helper;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly SkillRepo _skillRepo;
        public Regex skillIdRegex = new Regex(@"^SK\d{6}");

        public SkillController(IMapper mapper, SkillRepo skillRepo)
        {
            _mapper = mapper;
            _skillRepo = skillRepo;
        }

        [HttpGet("skills")]
        public IActionResult GetAllSkill()
        {
            try
            {
                var skillList = _mapper.Map<List<SkillDto>>(_skillRepo.GetAll());
                return Ok(skillList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("get/skill/{skillId}")]
        public IActionResult GetSkillById(string skillId)
        {
            try
            {
                if (!skillIdRegex.IsMatch(skillId))
                {
                    return BadRequest("Wrong skillId Format.");
                }
                var skill = _mapper.Map<SkillDto>(_skillRepo.GetAll().Where(s => s.SkillId == skillId).FirstOrDefault());
                if (skill == null)
                {
                    return BadRequest("Skill ID = " + skillId + " doesn't seem to be found.");
                }
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPost("create")]
        public IActionResult CreateSkill([FromBody] SkillDto skill)
        {
            try
            {
                if (skill == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!skillIdRegex.IsMatch(skill.SkillId))
                {
                    return BadRequest("Wrong skillId Format.");
                }
                if (_skillRepo.GetAll().Any(s => s.SkillId == skill.SkillId))
                {
                    return BadRequest("Skill ID = " + skill.SkillId + " existed");
                }
                var temp = _mapper.Map<Skill>(skill);
                _skillRepo.Add(temp);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpPut("update/skill/{skillId}")]
        public IActionResult UpdateSkill(string skillId, [FromBody] UpdateSkillDto skill)
        {
            if (skill == null)
            {
                return BadRequest("Some input information is null");
            }
            if (!skillIdRegex.IsMatch(skillId))
            {
                return BadRequest("Wrong skillId Format.");
            }
            var validSkill = _skillRepo.GetAll().Where(s => s.SkillId == skillId).FirstOrDefault();
            if (validSkill == null)
            {
                return BadRequest("Skill ID = " + skillId + " doesn't seem to be found.");
            }
            _mapper.Map(skill, validSkill);
            validSkill.SkillId = skillId;

            _skillRepo.Update(validSkill);
            return Ok(validSkill);
        }

        [HttpGet("skills/search/{keyword}")]
        public IActionResult FindProject(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<SkillDto>>(_skillRepo.GetAll().Where(s => RemoveVietnameseSign.RemoveSign(s.SkillName).ToLower().Contains(keyword.ToLower())));
                if (resultList == null)
                {
                    return BadRequest("No active skill(s) found");
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
