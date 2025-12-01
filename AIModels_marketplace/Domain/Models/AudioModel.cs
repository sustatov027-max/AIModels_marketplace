using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    public class AudioModel : AIModel
    {
        public int SampleRate { get; set; }
        public AudioModel()
        {
            SampleRate = 0;
        }
        public AudioModel(string name, string description, ModelMetadata metadata, List<ModelVersion> versions, int sampleRate): base(name, description, metadata, versions) 
        {
            SampleRate = sampleRate;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Тип: {this.GetType()},\n название: {Name},\n описание: {Description},\n id разработчика: {Metadata.AuthorId},\n кол-во версий: {Versions.Count()},\n частота дискретизации: {SampleRate}\n");
        }
    }
}
