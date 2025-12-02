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
                _models = _storage.Load<AIModel>(_filename) ?? new List<AIModel>();
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

            var current = Models;
            modelToAdd.Id = GenerateNewId();
            current.Add(modelToAdd);
            _storage.Save(_filename, current);
            _models = current;
        }

        private int GenerateNewId()
        {
            var current = Models;
            if (!current.Any())
                return 1;

            return current.Max(u => u.Id) + 1;
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
            var current = Models;
            int index = current.FindIndex(m => m.Id == id);
            if (index < 0)
                throw new ArgumentException("Модель с данным id не найдена");

            newModel.Id = id;
            current[index] = newModel;
            _storage.Save(_filename, current);
            _models = current;
        }

        public void Delete(int id)
        {
            var current = Models;
            current.RemoveAll(m => m.Id == id);
            _storage.Save(_filename, current);
            _models = current;
        }
    }
}
