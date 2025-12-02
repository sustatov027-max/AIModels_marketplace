using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    public class TextModel : AIModel
    {
        public int MaxTokens { get; set; }

        public TextModel()
        {
            MaxTokens = 0;
        }

        public TextModel(string name, string description, ModelMetadata metadata, List<ModelVersion> versions, int maxTokens)
            : base(name, description, metadata, versions)
        {
            MaxTokens = maxTokens;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Тип: {this.GetType()},\n название: {Name},\n описание: {Description},\n категория: {Metadata?.Category},\n id разработчика: {Metadata?.AuthorId},\n кол-во версий: {Versions?.Count},\n MaxTokens: {MaxTokens}\n");
        }
    }
}
