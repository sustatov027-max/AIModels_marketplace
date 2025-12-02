using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AIModels_marketplace.Domain.Models;
using AIModels_marketplace.Domain.Interfaces;

namespace AIModels_marketplace.Views
{
    public partial class ModelEditorWindow : Window
    {
        private readonly int _authorId;
        private readonly AIModel _editingModel;

        public IAIModel ResultModel { get; private set; }

        public ModelEditorWindow(int authorId, AIModel modelToEdit = null)
        {
            InitializeComponent();
            _authorId = authorId;
            _editingModel = modelToEdit;

            if (_editingModel != null)
            {
                NameTextBox.Text = _editingModel.Name;
                DescriptionTextBox.Text = _editingModel.Description;
                var latest = _editingModel.GetLatestVersion();
                VersionTextBox.Text = latest?.VersionNumber ?? "";
                VersionNotesTextBox.Text = latest?.Changelog ?? "";

                var category = _editingModel.Metadata?.Category ?? "Vision";
                if (category == "Vision") TypeComboBox.SelectedIndex = 0;
                else if (category == "Audio") TypeComboBox.SelectedIndex = 1;
                else if (category == "Text") TypeComboBox.SelectedIndex = 2;
                else TypeComboBox.SelectedIndex = 0;

                if (_editingModel is VisionModel vision)
                {
                    MaxResolutionTextBox.Text = vision.MaxResolution;
                }
                else if (_editingModel is AudioModel audio)
                {
                    SampleRateTextBox.Text = audio.SampleRate.ToString();
                }
                else if (_editingModel is TextModel text)
                {
                    MaxTokensTextBox.Text = text.MaxTokens.ToString();
                }
            }
            else
            {
                TypeComboBox.SelectedIndex = 0;
            }

            UpdatePanels();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePanels();
        }

        private void UpdatePanels()
        {
            var type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content as string;
            VisionPanel.Visibility = type == "Vision" ? Visibility.Visible : Visibility.Collapsed;
            AudioPanel.Visibility = type == "Audio" ? Visibility.Visible : Visibility.Collapsed;
            TextPanel.Visibility = type == "Text" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text?.Trim();
                string desc = DescriptionTextBox.Text?.Trim();
                string versionNum = VersionTextBox.Text?.Trim();
                string versionNotes = VersionNotesTextBox.Text?.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Введите название модели.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                var versions = _editingModel?.Versions != null
                    ? new List<ModelVersion>(_editingModel.Versions)
                    : new List<ModelVersion>();

                if (!string.IsNullOrWhiteSpace(versionNum))
                    versions.Add(new ModelVersion(versionNum, versionNotes ?? ""));

                string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content as string ?? "Vision";

                var parameters = new Dictionary<string, string>() { { "created", DateTime.UtcNow.ToString("o") } };
                
                ModelMetadata metadata = new ModelMetadata(type, _authorId, parameters);

                AIModel model;
                if (type == "Vision")
                {
                    string maxRes = MaxResolutionTextBox.Text?.Trim() ?? "";
                    model = new VisionModel(name, desc, metadata, versions, maxRes);
                }
                else if (type == "Audio")
                {
                    int sampleRate = 0;
                    int.TryParse(SampleRateTextBox.Text, out sampleRate);
                    model = new AudioModel(name, desc, metadata, versions, sampleRate);
                }
                else 
                {
                    int maxTokens = 0;
                    int.TryParse(MaxTokensTextBox.Text, out maxTokens);
                    model = new TextModel(name, desc, metadata, versions, maxTokens);
                }

                ResultModel = model;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}