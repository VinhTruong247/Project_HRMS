using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class EmployeeRepo : BaseRepository.BaseRepository<Employee>
    {
        public Employee GetAnEmployee(string empId)
        {
            return _context.Employees.Include(e => e.Job).Where(e => e.EmployeeId == empId).FirstOrDefault() ?? new Employee();
        }
    }
}
