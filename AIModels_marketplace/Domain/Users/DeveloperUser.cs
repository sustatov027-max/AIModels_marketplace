using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Users
{
    internal class DeveloperUser: UserBase
    {
        public DeveloperUser(string username, string passwordHash, string role) : base(username, passwordHash, role)
        {
        }

        public List<string> MyModelsIds {  get; set; }
    }
}
