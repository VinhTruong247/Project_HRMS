using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.SkillEmployee
{
    public class SkillEmployeeDto
    {
        [Required]
        public string UniqueId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string SkillId { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
