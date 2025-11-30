using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IModelRepository
    {
        void Add(IAIModel model);
        IAIModel Get(int id);
        List<IAIModel> GetAll();
        void Update(int id, AIModel model);
        void Delete(int id);
    }
}
