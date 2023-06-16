using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class SkillEmployee
    {
        public int UniqueId { get; set; }
        public int? EmployeeId { get; set; }
        public string Level { get; set; }
        public int? SkillId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
