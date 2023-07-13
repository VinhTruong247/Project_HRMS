using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class JobRepo : BaseRepository.BaseRepository<Job>
    {
        public decimal GetBaseSalary(string employeeId, decimal actualWorkHour)
        {
            var employee = _context.Employees.Include(e => e.Job).Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            decimal baseSalaryPerHour = employee.Job.BaseSalaryPerHour ?? 0;
            return baseSalaryPerHour * actualWorkHour;
        }
        public decimal? GetBonus(string employeeId)
        {
            var employee = _context.Employees.Include(e => e.Job).Where(e => e.EmployeeId == employeeId).FirstOrDefault();
            return (decimal)employee.Job.Bonus;
        }
    }
}
