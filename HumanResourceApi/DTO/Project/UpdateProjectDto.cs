using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Project
{
    public class UpdateProjectDto
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
