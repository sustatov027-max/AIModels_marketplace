using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AIModels_marketplace.Domain.Interfaces;
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
            IUserRepository userRepository = new JsonUserRepository();

            var (user, login) = authService.Login("mihan78022", "qwerty1234");
            Console.WriteLine($"Вход за пользователя {user.Username}, статус {login}");

        }

    }
}
