using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    public class TextModel: AIModel
    {
        public int ContextSize { get; set; }

        public TextModel()
        {
            ContextSize = 0;
        }

        public TextModel(string name, string description, ModelMetadata metadata, List<ModelVersion> versions, int contextSize) : base(name, description, metadata, versions)
        {
            ContextSize = contextSize;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Тип: {this.GetType()},\n название: {Name},\n описание: {Description},\n id разработчика: {Metadata.AuthorId},\n кол-во версий: {Versions.Count()},\n размер: {ContextSize}\n");
        }
    }
}
