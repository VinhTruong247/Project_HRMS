using HumanResourceApi.DTO;
using HumanResourceApi.DTO.Users;
using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class UserRepo : BaseRepository.BaseRepository<User>
    {
        public User CheckLogin(LoginDto loginInfo)
        {
            return _context.Users.Where(u => u.Username.ToUpper().Equals(loginInfo.Username.ToUpper()) 
            && u.Password.Equals(loginInfo.Password) && u.Status == "1").FirstOrDefault();
        }

        public Employee getEmployee(string id)
        {
            var user = _context.Users.Where(u => u.UserId == id).FirstOrDefault();
            var empId = user.EmployeeId;
            return _context.Employees.Where(e => e.EmployeeId ==  empId).FirstOrDefault();
        }

        public Role GetRole(string? roleId)
        {
            return _context.Roles.Where(r => r.RoleId == roleId).FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbSet.Include(u => u.Role).ToList();
        }

    }
}
