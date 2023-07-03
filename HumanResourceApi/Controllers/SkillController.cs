using AutoMapper;
using HumanResourceApi.DTO.Skill;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly SkillRepo _skillRepo;

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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/skill/{skillId}")]
        public IActionResult GetSkillById(string skillId)
        {
            var skill = _mapper.Map<SkillDto>(_skillRepo.GetAll().Where(s => s.SkillId == skillId).FirstOrDefault());
            if (skill == null)
            {
                return BadRequest();
            }
            return Ok(skill);
        }

        [HttpPost("create")]
        public IActionResult CreateSkill([FromBody] SkillDto skill)
        {
            if (skill == null)
            {
                return BadRequest();
            }
            if (_skillRepo.GetAll().Any(s => s.SkillId == skill.SkillId))
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Skill>(skill);
            _skillRepo.Add(temp);
            return Ok(skill);
        }

        [HttpPut("update/skill/{skillId}")]
        public IActionResult UpdateSkill(string skillId, [FromBody] UpdateSkillDto skill)
        {
            if (skill == null)
            {
                return BadRequest();
            }
            var validSkill = _skillRepo.GetAll().Where(s => s.SkillId == skillId).FirstOrDefault();
            if (validSkill == null)
            {
                return BadRequest();
            }
            _mapper.Map(skill, validSkill);
            validSkill.SkillId = skillId;

            _skillRepo.Update(validSkill);
            return Ok(validSkill);
        }

        //[HttpPost("delete")]
        //public IActionResult DeleteSkill([FromQuery] string id)
        //{
        //    var skill = _mapper.Map<SkillDto>(_skillRepo.GetAll().Where(s => s.SkillId == id).FirstOrDefault());
        //    if (skill == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (skill.Status == "Disable")
        //    {
        //        return BadRequest("ID = " + id + " is already disabled");
        //    }
        //    var validSkill = _skillRepo.GetAll().Where(s => s.SkillId == id).FirstOrDefault();
        //    _mapper.Map(skill, validSkill);
        //    validSkill.Status = "Disable";
        //    _skillRepo.Update(validSkill);
        //    return Ok(validSkill);
        //}
    }
}
