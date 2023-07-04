using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.SkillEmployee
{
    public class UpdateSkillEmployeeDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string SkillId { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
