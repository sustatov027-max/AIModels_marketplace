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
    internal abstract class UserBase: IUser
    {
        private static int _lastId = 0;
        private static readonly object _lock = new object();

        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public UserBase() { }
        public UserBase(string username, string passwordHash, string role) 
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

    }
}
