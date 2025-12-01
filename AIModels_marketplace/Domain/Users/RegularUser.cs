using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Infrastructure.Json;

namespace AIModels_marketplace.Domain.Users
{
    public class RegularUser: UserBase, IRegularUserModelService
    {
        public RegularUser() { }
        private JsonModelRepository _jsonModelRepository = new JsonModelRepository();
        private List<IAIModel> _models;
        public List<IAIModel> Models
        {
            get
            {
                _models = _jsonModelRepository.GetAll();
                return _models;
            }
            set
            {
                _models = value;
            }
        }
        public RegularUser(string username, string passwordHash, string role)
           : base(username, passwordHash, role) {
            Role = "User";
        }

        public IAIModel GetModel(int id)
        {
            if (Models.FirstOrDefault(m => m.Id == id) == null)
            {
                throw new ArgumentException("Модель с данным id не найдена");
            }
            return Models.FirstOrDefault(m => m.Id == id);
        }

        public List<IAIModel> GetAllModels()
        {
            return Models;
        }
    }
}
