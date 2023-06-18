namespace HumanResourceApi.DTO.Users
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
    }
}
