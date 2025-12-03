using System;
using System.Windows;
using System.Windows.Controls;
using AIModels_marketplace.Services;
using AIModels_marketplace.Domain.Interfaces;

namespace AIModels_marketplace.Views
{
    public partial class RegisterPage : Page
    {
        private readonly IAuthService _authService;

        public RegisterPage()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = UsernameTextBox.Text?.Trim();
                string password = PasswordBox.Password;
                var selected = RoleComboBox.SelectedItem as ComboBoxItem;
                string role = selected?.Content?.ToString() ?? "User";

                _authService.Register(username, password, role, null);
                MessageBox.Show("Регистрация прошла успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new LoginPage());
                    return;
                }

                NavigationService?.Navigate(new LoginPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new LoginPage());
                return;
            }
            NavigationService?.Navigate(new LoginPage());
        }

    }
}