using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class User
    {
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public bool? Status { get; set; } = true;

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
