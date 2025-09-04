using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public interface IUserRepo
    {
        void AddUser(User user);
        void DeleteUser(int uid);
        IEnumerable<User> GetAllUsers();
        User GetUSer(string email, string password);
        User GetUserById(int uid);
        void UpdateUser(User user);
        void AddRefreshToken(RefreshToken token);
        RefreshToken GetRefreshToken(string token);
        void DeleteRefreshToken(string token);
        User? GetUserByUsername(string username);
        IEnumerable<User> GetAllUsersWithTokens();
        User GetUserByEmail(string email);
    }
}