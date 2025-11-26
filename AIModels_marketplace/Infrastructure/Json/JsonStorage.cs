using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Xml;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Users;
using Newtonsoft.Json;

namespace AIModels_marketplace.Infrastructure.Json
{
    internal class JsonStorage : IStorage
    {
        private static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore
        };
        public List<T> Load<T>(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    string jsonString = File.ReadAllText(filename);
                    return JsonConvert.DeserializeObject<List<T>>(jsonString, Settings) ?? new List<T>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке пользователей: {ex.Message}");
            }

            return new List<T>();
        }

        public void Save<T>(string filename, List<T> items)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(items, Settings);
                File.WriteAllText(filename, jsonString);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Ошибка при сохранении в файл", ex);
            }
        }
    }
}   