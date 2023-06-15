using HumanResourceApi.Interfaces;
using HumanResourceApi.Models;

namespace HumanResourceApi.Repositories
{
    public class UserRepository : IUser
    {
        private readonly HRMSContext _context;

        public UserRepository(HRMSContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        
        public bool CheckLogin(string username, string password)
        {
            return _context.Users.Any(u => u.Username.ToUpper().Equals(username.ToUpper()) && u.Password.Equals(password));
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }
    }
}
