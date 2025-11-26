using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Users;
using AIModels_marketplace.Infrastructure.Json;

namespace AIModels_marketplace.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService()
        {
            _userRepository = new JsonUserRepository();
        }
        public void Register(string username, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Имя пользователя не может быть пустым");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым");

            if (role != "Developer" && role != "User")
                throw new ArgumentException("Роль может быть только Admin или User");

            string hashed = HashPassword(password);

            IUser user;

            switch (role)
            {
                case "Developer":
                    user = new DeveloperUser(username, password, role);
                    break;
                case "User":
                    user = new RegularUser(username, password, role);
                    break;
                default:
                    user = null;
                    break;
            }

            _userRepository.Add(user);
        }

        public (IUser, bool) Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Имя пользователя не может быть пустым");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым");
            string EnterPasswordHash = HashPassword(password);

            List<IUser> users = _userRepository.GetAll() ?? new List<IUser>();

            foreach (var user in users)
            {
                if ((user.Username == username) && (user.PasswordHash == EnterPasswordHash))
                {
                    return (user, true);
                }
            }

            return (null, false);
        }

        public static string HashPassword(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("FixedSalt");

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 1000))
            {
                byte[] hash = deriveBytes.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
