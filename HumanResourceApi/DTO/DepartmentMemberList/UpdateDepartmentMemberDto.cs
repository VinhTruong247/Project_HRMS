using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.DepartmentMemberList
{
    public class UpdateDepartmentMemberDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string EmpRole { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
