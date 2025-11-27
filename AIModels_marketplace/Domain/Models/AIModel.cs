using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;

namespace AIModels_marketplace.Domain.Models
{
    internal abstract class AIModel: IAIModel, IVersioned
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ModelMetadata Metadata { get; set; }
        public List<ModelVersion> Versions { get; set; }

        public void AddVersion(ModelVersion version)
        {
            Versions.Add(version);
        }

        public ModelVersion GetLatestVersion()
        {
            return Versions.LastOrDefault();
        }

        public AIModel() { }
        public AIModel(string name, string description, ModelMetadata metadata, List<ModelVersion> versions)
        {
            Name = name; 
            Description = description; 
            Metadata = metadata; 
            Versions = versions; 
        }
    }
}
