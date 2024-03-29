﻿using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Leave
{
    public class LeaveDto
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string LeaveType { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public double? LeaveHours { get; set; }
    }
}
