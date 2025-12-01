using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IRegularUserModelService
    {
        IAIModel GetModel(int id);
        List<IAIModel> GetAllModels();
    }
}
