using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class GrantedPermission
    {
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
        public bool? Status { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
