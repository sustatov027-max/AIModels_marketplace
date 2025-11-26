using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Users
{
    [JsonDerivedType(typeof(UserBase), typeDiscriminator: "user")]
    [JsonDerivedType(typeof(DeveloperUser), typeDiscriminator: "admin")]
    internal class DeveloperUser: UserBase
    {
        public List<int> ModelsIds {  get; set; }

        public DeveloperUser(string username, string passwordHash, string role) : base(username, passwordHash, role)
        {
            ModelsIds = new List<int>();
            Role = "Developer";
        }

        public DeveloperUser()
        {
            ModelsIds = new List<int>();
        }

       
    }
}
