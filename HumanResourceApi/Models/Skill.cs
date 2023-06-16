using System;
using System.Collections.Generic;

namespace HumanResourceApi.Models
{
    public partial class Skill
    {
        public Skill()
        {
            SkillEmployees = new HashSet<SkillEmployee>();
        }

        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillDescription { get; set; }

        public virtual ICollection<SkillEmployee> SkillEmployees { get; set; }
    }
}
