using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Infrastructure.Json;

namespace AIModels_marketplace.Domain.Models
{
    public abstract class AIModel: IAIModel, IVersioned
    {

        private JsonModelRepository _jsonModelRepository = new JsonModelRepository();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ModelMetadata Metadata { get; set; }
        public List<ModelVersion> Versions { get; set; }

        public void AddVersion(ModelVersion version)
        {
            Versions.Add(version);
            _jsonModelRepository.Update(Id, this);
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

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Название: {Name},\n описание: {Description},\n id разработчика: {Metadata.AuthorId},\n кол-во версий: {Versions.Count}");
        }
    }
}
