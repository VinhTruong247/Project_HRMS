using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace HumanResourceApi.Repositories
{
    public class AttendanceRepo : BaseRepository.BaseRepository<Attendance>
    {
        public TimeSpan? GetActualHours(string employeeId, DateTime month)
        {
            var attendanceList = _dbSet.Where(a => a.EmployeeId == employeeId 
            && a.Day.Year == month.Year && a.Day.Month == month.Month).ToList();

            TimeSpan? actualHour = TimeSpan.Zero;
            attendanceList.ForEach(a =>
            {
                var tempHour = a.TotalHours ?? TimeSpan.Zero;
                actualHour += tempHour;
            });
            return actualHour;
        }
    }
}
