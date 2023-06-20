using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Permission
    {
        public string PermissionId { get; set; }
        public string PermissionDes { get; set; }
        public string PermissionDisplayName { get; set; }
        public string Status { get; set; }
    }
}
