using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Domain.Interfaces
{
    internal interface IAIModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ModelMetadata Metadata { get; set; }
        List<ModelVersion> Versions { get; set; }
        void AddVersion(ModelVersion v);
        ModelVersion GetLatestVersion();
    }
}
