using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Department
{
    public class DepartmentDto
    {
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
