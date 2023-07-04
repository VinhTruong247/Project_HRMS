using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Department
{
    public class UpdateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
