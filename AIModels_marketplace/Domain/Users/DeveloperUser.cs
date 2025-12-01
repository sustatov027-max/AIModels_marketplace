using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Models;
using AIModels_marketplace.Infrastructure.Json;

namespace AIModels_marketplace.Domain.Users
{
    public class DeveloperUser: UserBase, IDeveloperModelService
    {
        private JsonModelRepository _modelsRepository = new JsonModelRepository();
        private List<IAIModel> _developerModels;

        [Newtonsoft.Json.JsonIgnore]
        public List<IAIModel> DeveloperModels
        {
            get {
                _developerModels = _modelsRepository.GetAll().Where(m => m.Metadata.AuthorId == Id).ToList();
                return _developerModels;
            }
            set
            {
                _developerModels = value;
            }
        }

        public DeveloperUser(string username, string passwordHash, string role) : base(username, passwordHash, role)
        {
            Role = "Developer";
        }

        public DeveloperUser()
        {
        }

        public void CreateModel(IAIModel model)
        {
            if (DeveloperModels.Any(m => m.Name == model.Name))
            {
                throw new ArgumentException("Данная модель уже добавлена");
            }

            _modelsRepository.Add(model);
            DeveloperModels.Add(model);

        }

        public List<IAIModel> GetAllModels()
        {
            return DeveloperModels;
        }

        public IAIModel GetModel(int id)
        {
            if (DeveloperModels.FirstOrDefault(m => m.Id == id) == null)
            {
                throw new ArgumentException("Модель с данным id не найдена");
            }
            return DeveloperModels.FirstOrDefault(m => m.Id == id);
        }

        public void UpdateModel(int id, IAIModel model)
        {
            if (DeveloperModels.FirstOrDefault(m => m.Id == id) == null)
            {
                throw new ArgumentException("Модель с данным id не найдена");
            }

            _modelsRepository.Update(id, model as AIModel);
        }

        public void DeleteModel(int id)
        {
            if (DeveloperModels.FirstOrDefault(m => m.Id == id) == null)
            {
                throw new ArgumentException("Модель с данным id не найдена");
            }
            _modelsRepository.Delete(id);
        }
    }
}
