namespace HumanResourceApi.DTO.Users
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? RoleId { get; set; }
        public bool? Status { get; set; }
    }
}