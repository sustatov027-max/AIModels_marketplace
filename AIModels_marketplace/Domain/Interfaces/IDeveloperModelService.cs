using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IDeveloperModelService
    {
        void CreateModel(IAIModel model);
        IAIModel GetModel(int id);
        List<IAIModel> GetAllModels();
        void UpdateModel(int id, IAIModel model);
        //void DeleteModel(int id);
    }
}
