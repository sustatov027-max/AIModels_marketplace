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
        List<T> Load<T>(string filename);
        void Save<T>(string filename, List<T> items);
    }
}
