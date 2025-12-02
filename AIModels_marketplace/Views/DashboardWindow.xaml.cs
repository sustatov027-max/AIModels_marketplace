using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AIModels_marketplace.Domain.Users;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Views
{
    public partial class DashboardWindow : Window
    {
        private readonly UserBase _user;
        private readonly DeveloperUser _developer;

        public DashboardWindow(UserBase user)
        {
            InitializeComponent();
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _developer = _user as DeveloperUser;

            WelcomeTextBlock.Text = $"Добро пожаловать, {_user.Username} ({_user.Role})";

            UpdateButtonsVisibility();
            LoadModels();
        }

        private void UpdateButtonsVisibility()
        {
            bool isDev = _developer != null;
            CreateButton.Visibility = isDev ? Visibility.Visible : Visibility.Collapsed;
            EditButton.Visibility = isDev ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = isDev ? Visibility.Visible : Visibility.Collapsed;
        }

        private void LoadModels()
        {
            try
            {
                if (_developer != null)
                {
                    var devModels = _developer.GetAllModels() ?? new List<IAIModel>();
                    ModelsListBox.ItemsSource = devModels;
                }
                else if (_user is RegularUser regular)
                {
                    var regularModels = regular.GetAllModels() ?? new List<IAIModel>();
                    ModelsListBox.ItemsSource = regularModels;
                }
                else
                {
                    ModelsListBox.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки моделей", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadModels();
        }

        private void ShowInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = ModelsListBox.SelectedItem as IAIModel;
            if (selected == null)
            {
                MessageBox.Show("Выберите модель из списка.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var info = new ModelInfoWindow(selected) { Owner = this };
                info.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка отображения информации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
            this.Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_developer == null) return;

            var editor = new ModelEditorWindow(_developer.Id) { Owner = this };
            if (editor.ShowDialog() == true && editor.ResultModel != null)
            {
                try
                {
                    _developer.CreateModel(editor.ResultModel);
                    LoadModels();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка создания", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_developer == null) return;

            var selected = ModelsListBox.SelectedItem as AIModel;
            if (selected == null)
            {
                MessageBox.Show("Выберите модель для редактирования.", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editor = new ModelEditorWindow(_developer.Id, selected) { Owner = this };
            if (editor.ShowDialog() == true && editor.ResultModel != null)
            {
                try
                {
                    _developer.UpdateModel(selected.Id, editor.ResultModel);
                    LoadModels();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_developer == null) return;

            var selected = ModelsListBox.SelectedItem as AIModel;
            if (selected == null)
            {
                MessageBox.Show("Выберите модель для удаления.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var res = MessageBox.Show($"Удалить модель \"{selected.Name}\" (id = {selected.Id})?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res != MessageBoxResult.Yes) return;

            try
            {
                _developer.DeleteModel(selected.Id);
                LoadModels();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}