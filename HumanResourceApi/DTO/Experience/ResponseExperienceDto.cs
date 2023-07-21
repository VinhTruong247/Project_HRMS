using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Experience
{
    public class ResponseExperienceDto
    {
        [Required]
        public string ExperienceId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string NameProject { get; set; }
        [Required]
        public int? TeamSize { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string TechStack { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
