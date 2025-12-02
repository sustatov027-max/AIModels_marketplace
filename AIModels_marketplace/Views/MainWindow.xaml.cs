using System.Windows;
using System.Windows.Controls;

namespace AIModels_marketplace.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            MainFrame.Navigate(new LoginPage());
        }

        private void BtnLoginPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

        private void BtnRegisterPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RegisterPage());
        }
    }
}