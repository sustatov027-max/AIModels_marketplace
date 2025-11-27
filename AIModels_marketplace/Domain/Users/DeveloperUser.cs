using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Users
{
    internal class DeveloperUser: UserBase
    {
        public List<int> ModelsIds {  get; set; }

        public DeveloperUser(string username, string passwordHash, string role, List<int> modelsIds) : base(username, passwordHash, role)
        {
            ModelsIds = modelsIds;
            Role = "Developer";
        }

        public DeveloperUser()
        {
            ModelsIds = new List<int>();
        }

       
    }
}
