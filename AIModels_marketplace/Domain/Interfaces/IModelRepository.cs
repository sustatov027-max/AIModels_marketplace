using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Interfaces
{
    internal interface IModelRepository
    {
        void Add(IAIModel model);
       /* IAIModel Get(int id);
        List<IAIModel> GetAll();
        void Update(IAIModel model);
        void Delete(int id);*/
    }
}
