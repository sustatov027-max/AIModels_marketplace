using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Domain.Interfaces
{
    public interface IVersioned
    {
        void AddVersion(ModelVersion v);
        ModelVersion GetLatestVersion();
    }
}
