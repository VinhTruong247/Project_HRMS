using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.DepartmentMemberList
{
    public class DepartmentMemberDto
    {
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string EmpRole { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
