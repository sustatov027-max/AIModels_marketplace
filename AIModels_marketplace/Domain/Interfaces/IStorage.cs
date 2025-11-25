using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Models;
using AIModels_marketplace.Domain.Users;

namespace AIModels_marketplace.Domain.Interfaces
{
    internal interface IStorage
    {
        List<UserBase> Users { get; set; }
        List<IAIModel> Models { get; set; }
        List<IAIModel> LoadModels();
        void SaveModels(List<IAIModel> models);

        List<UserBase> LoadUsers();
        void SaveUsers(IUser user);
    }
}
