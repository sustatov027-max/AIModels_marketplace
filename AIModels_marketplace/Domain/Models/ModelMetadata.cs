using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIModels_marketplace.Domain.Models
{
    public class ModelMetadata
    {
        public string Category { get; set; } // Text, Vision, Audio
        public int AuthorId { get; set; }

        public Dictionary<string, string> Parameters { get; set; }


        public ModelMetadata(string category, int authorId, Dictionary<string, string> parameters) 
        {
            Category = category;
            AuthorId = authorId;
            Parameters = parameters;
        }
    }
}
