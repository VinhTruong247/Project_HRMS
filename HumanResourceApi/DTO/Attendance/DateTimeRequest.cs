using System.ComponentModel.DataAnnotations;

namespace HumanResourceApi.DTO.Attendance
{
    public class DateTimeRequest
    {
        [Required]
        public DateTime? Day { get; set; }
    }
}
