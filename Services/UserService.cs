using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public void AddUser(User user)
        {
            _userRepo.AddUser(user);
        }

        public void DeleteUser(int uid)
        {
            var user = _userRepo.GetUserById(uid);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {uid} not found.");

            _userRepo.DeleteUser(uid);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }
        public User GetUSer(string email, string password)
        {
            var user = _userRepo.GetUSer(email, password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return user;
        }
        public User GetUserById(int uid)
        {
            var user = _userRepo.GetUserById(uid);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {uid} not found.");
            return user;
        }
        public void UpdateUser(User user)
        {
            var existingUser = _userRepo.GetUserById(user.UID);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {user.UID} not found.");

            _userRepo.UpdateUser(user);
        }

        public User? GetUserByUsername(string username)
        {
            return _userRepo.GetUserByUsername(username);
        }

        public void RegisterUser(string username, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                UName = username,
                PasswordHash = hashedPassword
            };
            _userRepo.AddUser(user);
        }

        public bool VerifyUser(string username, string password)
        {
            var user = _userRepo.GetUserByUsername(username);
            if (user == null) return false;

            bool verified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return verified;
        }
        public RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(7),
                UID = user.UID,
                IsRevoked = false
            };

            _userRepo.AddRefreshToken(refreshToken);
            return refreshToken;
        }

        public User ValidateRefreshToken(string token)
        {
            var refreshToken = _userRepo.GetRefreshToken(token);
            if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow || refreshToken.IsRevoked)
                return null;

            return _userRepo.GetUserById(refreshToken.UID);
        }

    }
}

