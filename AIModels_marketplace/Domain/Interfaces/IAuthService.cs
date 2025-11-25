using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IAuthService
    {
        (IUser, bool) Login (string username, string password);
        void Register(string username, string password, string role);
    }
}
