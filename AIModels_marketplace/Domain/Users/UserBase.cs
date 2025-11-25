using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;

namespace AIModels_marketplace.Domain.Users
{
    internal class UserBase: IUser
    {
        private static int _lastId = -1;
        public int Id { get; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        [JsonConstructor]
        public UserBase() { }
        public UserBase(string username, string passwordHash, string role) 
        {
            Id = GenerateId();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        static int GenerateId()
        {
            return Interlocked.Increment(ref _lastId);
        }
    }
}
