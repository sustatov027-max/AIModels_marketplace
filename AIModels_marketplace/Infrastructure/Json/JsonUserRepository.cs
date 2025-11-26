using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Users;

namespace AIModels_marketplace.Infrastructure.Json
{
    internal class JsonUserRepository: IUserRepository
    {
        private readonly JsonStorage _storage = new JsonStorage();
        private List<UserBase> _users;
        private string _filename = "users.json";

        public List<UserBase> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = _storage.Load<UserBase>(_filename) ?? new List<UserBase>();
                }
                return _users;
            }
            set { _users = value; }
        }
        public void Add(IUser user)
        {
            if (Users.Any(u => u.Username == user.Username))
            {
                throw new ArgumentException("Пользователь с данным именем уже существует");
            }

            UserBase userToAdd = user as UserBase;
            if (userToAdd == null)
            {
                throw new ArgumentException($"Неизвестный тип пользователя: {user.GetType()}");
            }

            userToAdd.Id = GenerateNewId();
            Users.Add(userToAdd);
            _storage.Save(_filename, Users);
        }

        private int GenerateNewId()
        {
            if (!Users.Any())
                return 1;

            return Users.Max(u => u.Id) + 1;
        }

        public IUser Get(string username)
        {
            return Users.FirstOrDefault(x => x.Username == username);
        }

        public List<IUser> GetAll()
        {
            return Users.Cast<IUser>().ToList();
        }
    }
}
