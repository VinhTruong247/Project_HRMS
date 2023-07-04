namespace HumanResourceApi.DTO.Users
{
    public class UpdateUserDto
    {
        //public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public bool? Status { get; set; }
    }
}