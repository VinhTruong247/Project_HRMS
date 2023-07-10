using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Job
{
    public class JobDto
    {
        [Required]
        public string JobId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string JobDescription { get; set; }
        
        public DateTime? StartDate { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public decimal? BaseSalaryPerHour { get; set; }
        [Required]
        public string AllowanceId { get; set; }
        
        public decimal? Bonus { get; set; }
    }
}
