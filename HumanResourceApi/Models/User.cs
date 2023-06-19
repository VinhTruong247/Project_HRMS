using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
            Status = "active";
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string Status { get; set; }
        

        public virtual Role Role { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
