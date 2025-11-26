using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Infrastructure.Json;

namespace AIModels_marketplace.Domain.Users
{
    internal class UserBase: IUser
    {
        private static int _lastId = 0;
        private static bool _isInitialized = false;
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        [JsonConstructor]
        public UserBase() { }
        public UserBase(string username, string passwordHash, string role) 
        {
            InitializeLastId();
            Id = GenerateId();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        private static void InitializeLastId()
        {
            if (!_isInitialized) {
                try
                {
                    var storage = new JsonStorage();
                    var users = storage.LoadUsers();
                    if (users != null && users.Any())
                    {
                        _lastId = users.Max(u => u.Id);
                    }
                    _isInitialized = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при инициализации ID: {ex.Message}");
                    _lastId = 0;
                    _isInitialized = true;
                }
            }
        }

        static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }
    }
}
