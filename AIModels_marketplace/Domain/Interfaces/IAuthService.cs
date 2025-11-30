using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Users;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IAuthService
    {
        (UserBase, bool) Login (string username, string password);
        void Register(string username, string password, string role, List<int> modelsIds = null);
    }
}
