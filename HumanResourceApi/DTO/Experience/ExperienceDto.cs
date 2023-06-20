﻿namespace HumanResourceApi.DTO.Experience
{
    public class ExperienceDto
    {
        public int ExperienceId { get; set; }
        public string NameProject { get; set; }
        public int? TeamSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TechStack { get; set; }
    }
}
