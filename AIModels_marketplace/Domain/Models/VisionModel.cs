using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    public class VisionModel: AIModel
    {
        public string MaxResolution { get; set; }

        public VisionModel() 
        {
            MaxResolution = String.Empty;
        }
        public VisionModel(string name, string description, ModelMetadata metadata, List<ModelVersion> versions, string maxResolution) : base(name, description, metadata, versions)
        {
            MaxResolution = maxResolution;
        }
    }
}
