﻿using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Job
{
    public class JobDto
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public decimal? BaseSalaryPerHour { get; set; }
        [Required]
        public decimal? Bonus { get; set; }
    }
}
