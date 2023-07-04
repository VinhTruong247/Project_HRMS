using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Employee
{
    public class EmployeeDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmployeeImage { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string EmployeeAddress { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int? BankAccountNumber { get; set; }
        [Required]
        public string BankAccountName { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string JobId { get; set; }
        [Required]
        public string DepartmentId { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
