using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Skill
{
    public class ResponseSkillDto
    {
        [Required]
        public string SkillId { get; set; }
        [Required]
        public string SkillName { get; set; }
        [Required]
        public string SkillDescription { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
}
