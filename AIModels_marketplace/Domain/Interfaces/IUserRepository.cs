using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Interfaces
{
    internal interface IUserRepository
    {
        void Add(IUser user);
        IUser Get(string username);
        List<IUser> GetAll();
    }
}
