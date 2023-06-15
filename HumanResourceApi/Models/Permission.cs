using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionDes { get; set; }
        public string PermissionDisplayName { get; set; }
    }
}
