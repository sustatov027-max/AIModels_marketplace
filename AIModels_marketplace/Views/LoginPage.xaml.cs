using System;
using System.Windows;
using System.Windows.Controls;
using AIModels_marketplace.Services;
using AIModels_marketplace.Domain.Users;
using AIModels_marketplace.Domain.Interfaces;

namespace AIModels_marketplace.Views
{
    public partial class LoginPage : Page
    {
        private readonly IAuthService _authService;

        public LoginPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text?.Trim();
                string password = PasswordBox.Password;

                var (user, success) = _authService.Login(username, password);
                if (success && user != null)
                {
                    
                    var dashboard = new DashboardWindow(user);
                    var mainWindow = Window.GetWindow(this) as Window;
                    dashboard.Owner = mainWindow;
                    dashboard.Show();
                    
                    mainWindow?.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoRegister_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new RegisterPage());
                return;
            }

            NavigationService?.Navigate(new RegisterPage());
        }

    }
}