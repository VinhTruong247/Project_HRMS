using HumanResourceApi.Models;

namespace HumanResourceApi.Interfaces
{
    public interface IUser
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        User CheckLogin(string username, string password);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        bool UserExists(int id);
        bool IsActive(int id);
        bool Save();
    }
}
