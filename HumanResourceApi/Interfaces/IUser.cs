using HumanResourceApi.Models;

namespace HumanResourceApi.Interfaces
{
    public interface IUser
    {
        ICollection<User> GetUsers();
        User GetUserById(int id);
        bool CheckLogin(string username, string password);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool UserExists(int id);
    }
}
