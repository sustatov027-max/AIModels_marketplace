using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    internal class TextModel: AIModel
    {
        public int ContextSize { get; set; }
    }
}
