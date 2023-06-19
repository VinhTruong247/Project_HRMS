using HumanResourceApi.DTO;
using HumanResourceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanResourceApi.Repositories
{
    public class UserRepo : BaseRepository.BaseRepository<User>
    {
        public User CheckLogin(string username, string password)
        {
            return _context.Users.Where(u => u.Username.ToUpper().Equals(username.ToUpper()) && u.Password.Equals(password) && u.Status == "active").FirstOrDefault();
        }

        public Employee getEmployee(int id)
        {
            return _context.Employees.Where(e => e.UserId ==  id).FirstOrDefault();
        }

        public Role GetRole(int? roleId)
        {
            return _context.Roles.Where(r => r.RoleId == roleId).FirstOrDefault();
        }

    }
}
