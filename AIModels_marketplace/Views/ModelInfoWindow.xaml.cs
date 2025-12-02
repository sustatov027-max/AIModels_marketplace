using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AIModels_marketplace.Domain.Interfaces;
using AIModels_marketplace.Domain.Models;

namespace AIModels_marketplace.Views
{
    public partial class ModelInfoWindow : Window
    {
        private readonly IAIModel _model;

        public ModelInfoWindow(IAIModel model)
        {
            InitializeComponent();
            _model = model ?? throw new ArgumentNullException(nameof(model));
            FillData();
        }

        private void FillData()
        {
            TitleTextBlock.Text = _model.GetType().Name + " — " + (_model.GetType().GetProperty("Name")?.GetValue(_model) as string ?? "(без названия)");
            DescriptionTextBlock.Text = "Описание: " + (_model.GetType().GetProperty("Description")?.GetValue(_model) as string ?? "");

            var metadataProp = _model.GetType().GetProperty("Metadata")?.GetValue(_model);
            if (metadataProp != null)
            {
                var category = metadataProp.GetType().GetProperty("Category")?.GetValue(metadataProp) as string ?? "";
                var authorId = metadataProp.GetType().GetProperty("AuthorId")?.GetValue(metadataProp)?.ToString() ?? "";
                MetadataTextBlock.Text = $"Категория: {category};\nАвтор (id): {authorId}";
                var parameters = metadataProp.GetType().GetProperty("Parameters")?.GetValue(metadataProp) as System.Collections.Generic.Dictionary<string, string>;
                ParametersPanel.Children.Clear();
                if (parameters != null)
                {
                    foreach (var kv in parameters)
                    {
                        ParametersPanel.Children.Add(new TextBlock() { Text = $"{kv.Key}: {kv.Value}", TextWrapping = TextWrapping.Wrap });
                    }
                }
            }

            VersionsList.ItemsSource = _model.GetType().GetProperty("Versions")?.GetValue(_model) is System.Collections.IEnumerable versions
                ? versions.Cast<object>().Select(v =>
                {
                    var num = v.GetType().GetProperty("VersionNumber")?.GetValue(v) as string ?? "";
                    var changelog = v.GetType().GetProperty("Changelog")?.GetValue(v) as string ?? "";
                    var date = v.GetType().GetProperty("ReleaseDate")?.GetValue(v) as DateTime?;
                    return $"Версия: {num}; Дата: {date?.ToString("u")}; Изменения: {changelog}";
                }).ToList()
                : null;

            SpecificPanel.Children.Clear();
            if (_model is VisionModel vm)
            {
                SpecificPanel.Children.Add(new TextBlock() { Text = $"MaxResolution: {vm.MaxResolution}" });
            }
            else if (_model is AudioModel am)
            {
                SpecificPanel.Children.Add(new TextBlock() { Text = $"SampleRate: {am.SampleRate}" });
            }
            else if (_model is TextModel tm)
            {
                SpecificPanel.Children.Add(new TextBlock() { Text = $"MaxTokens: {tm.MaxTokens}" });
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}