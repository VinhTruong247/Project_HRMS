using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class SkillEmployee
    {
        public string UniqueId { get; set; }
        public string EmployeeId { get; set; }
        public string Level { get; set; }
        public string SkillId { get; set; }
        public string Status { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
