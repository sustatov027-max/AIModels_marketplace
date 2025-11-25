using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using AIModels_marketplace.Domain.Interfaces;
using System.IO;
using AIModels_marketplace.Domain.Users;

namespace AIModels_marketplace.Infrastructure.Json
{
    internal class JsonStorage : IStorage
    {
        private List<UserBase> _users;

        public List<UserBase> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = LoadUsers() ?? new List<UserBase>();
                }
                return _users;
            }
            set { _users = value; }
        }

        public List<IAIModel> Models { get; set; }

        public List<UserBase> LoadUsers()
        {
            try
            {
                string filename = "users.json";
                if (File.Exists(filename))
                {
                    string jsonString = File.ReadAllText(filename);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<UserBase>>(jsonString, options) ?? new List<UserBase>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке пользователей: {ex.Message}");
            }

            return new List<UserBase>();
        }

        public void SaveUsers(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (Users.Any(u => u.Username == user.Username))
                throw new InvalidOperationException($"Пользователь с именем '{user.Username}' уже существует");

            Users.Add((UserBase)user);
            SaveToJsonFile();
        }

        private void SaveToJsonFile()
        {
            try
            {
                string filename = "users.json";
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                string jsonString = JsonSerializer.Serialize(Users, options);
                File.WriteAllText(filename, jsonString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка при сохранении в файл", ex);
            }
        }

        public List<IAIModel> LoadModels()
        {
            return null;
        }

        public void SaveModels(List<IAIModel> models)
        {
        }
    }
}