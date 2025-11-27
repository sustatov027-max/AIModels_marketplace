using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Infrastructure.Json
{
    internal class JsonModelRepository: IModelRepository
    {
        private readonly IStorage _storage = new JsonStorage();
        private List<AIModel> _models;
        private string _filename = "D:\\Documents\\2 курс\\ООП\\Курсовая\\AIModels_marketplace\\AIModels_marketplace\\Infrastructure\\Storage\\models.json";

        public List<AIModel> Models
        {
            get
            {
                if (_models == null)
                {
                    _models = _storage.Load<AIModel>(_filename) ?? new List<AIModel>();
                }
                return _models;
            }

            set { _models = value; }
        }

        public void Add(IAIModel model)
        {
            AIModel modelToAdd = model as AIModel;

            modelToAdd.Id = 0;
            Models.Add(modelToAdd);
            _storage.Save(_filename, Models);
        }
    }
}
