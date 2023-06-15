using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class GrantedPermission
    {
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
