using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Infrastructure.Json
{
    public class JsonModelRepository: IModelRepository
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
            if (modelToAdd == null)
            {
                throw new ArgumentException($"Неизвестный тип модели: {model.GetType()}");
            }

            modelToAdd.Id = GenerateNewId();
            Models.Add(modelToAdd);
            _storage.Save(_filename, Models);
        }

        private int GenerateNewId()
        {
            if (!Models.Any())
                return 1;

            return Models.Max(u => u.Id) + 1;
        }

        public IAIModel Get(int id)
        {
            return Models.FirstOrDefault(m => m.Id == id);
        }

        public List<IAIModel> GetAll()
        {
            return Models.Cast<IAIModel>().ToList();
        }

        public void Update(int id, AIModel newModel)
        {
            newModel.Id = id;
            Models[Models.FindIndex(m => m.Id == id)] = newModel;
            _storage.Save(_filename, Models);
        }

        public void Delete(int id)
        {
            Models.RemoveAll(m => m.Id == id);
            _storage.Save(_filename, Models);
        }
    }
}
