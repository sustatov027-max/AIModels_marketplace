using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Models;
using AIModels_marketplace.Domain.Users;
using AIModels_marketplace.Infrastructure.Json;
using AIModels_marketplace.Services;

namespace AIModels_marketplace
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IAuthService authService = new AuthService();

            List<ModelVersion> modelVersions = new List<ModelVersion>();
            modelVersions.Add(new ModelVersion("2.3f", "added new functions"));

            //IAIModel model = new VisionModel("Vision 2.0", "This is vision model",
            //    new ModelMetadata("Vision Model", 0, new Dictionary<string, string>() { { "config", "default" } }),
            //    modelVersions,
            //    "1080p");
            //modelRepository.Add(model);

            //IAIModel model2 = new VisionModel("Vision 2.7", "This is vision model",
            //    new ModelMetadata("Vision Model", 0, new Dictionary<string, string>() { { "config", "default" } }),
            //    modelVersions,
            //    "720p");
            //modelRepository.Add(model2);


            authService.Register("sirserg123", "2556625", "Developer");
            var (user, login) = authService.Login("sirserg123", "2556625");
            if (user is DeveloperUser developer)
            {
                IAIModel testModel = new VisionModel("The Best Model", "This is a new vision model from developer 2",
                new ModelMetadata("Vision Model", developer.Id, new Dictionary<string, string>() { { "config", "default" } }),
                modelVersions,
                "720p");

                developer.CreateModel(testModel);
                //developer.UpdateModel(2, updateModel);
                //developer.DeleteModel(2);

                foreach (IAIModel model in developer.GetAllModels())
                {
                    Console.WriteLine(model.Id);
                }
            }
        }

    }
}
