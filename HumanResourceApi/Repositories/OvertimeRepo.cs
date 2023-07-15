using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class OvertimeRepo : BaseRepository.BaseRepository<Overtime>
    {
        public decimal GetOTHours(string employeeId, DateTime month)
        {
            var otList = _dbSet.Where(o => o.EmployeeId == employeeId
                         && o.Day.Year == month.Year
                         && o.Day.Month == month.Month).ToList();
            decimal totalOt = 0;
            otList.ForEach(o =>
            {
                var otTime = o.OvertimeHours;
                totalOt += (decimal)otTime.TotalHours;
            });
            return totalOt;
        }

        public decimal GetOtSalary(decimal otHours, string employeeId)
        {
            var emp = _context.Employees.Include(e => e.Job).Where(e => e.EmployeeId == employeeId).FirstOrDefault();

            return otHours * emp.Job.BaseSalaryPerHour ?? 0;
        }
    }
}
