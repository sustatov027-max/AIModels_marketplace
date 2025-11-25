using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    internal class ModelMetadata
    {
        public string Category { get; set; } // Text, Vision, Audio
        public int AuthorId { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
