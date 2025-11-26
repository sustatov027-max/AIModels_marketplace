using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Users
{
    internal class RegularUser: UserBase
    {
        public RegularUser() { }
        public RegularUser(string username, string passwordHash, string role)
           : base(username, passwordHash, role) {
            Role = "User";
        }
    }
}
