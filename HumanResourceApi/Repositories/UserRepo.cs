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
            return _context.Employees.Where(e => e.UserId ==  id).FirstOrDefault();
        }

        public Role GetRole(string? roleId)
        {
            return _context.Roles.Where(r => r.RoleId == roleId).FirstOrDefault();
        }

    }
}
