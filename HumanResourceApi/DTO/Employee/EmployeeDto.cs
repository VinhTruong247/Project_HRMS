using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Employee
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeImage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmployeeAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string UserId { get; set; }
        public string JobId { get; set; }
        public string DepartmentId { get; set; }
        public string Status { get; set; }
    }
}
