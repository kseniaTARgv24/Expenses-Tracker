using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Expenses_Tracker.Models;
using Expenses_Tracker.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Expenses_Tracker.ViewModels
{
    public partial class AddCategoryViewModel : ObservableObject
    {
        private readonly IDatabaseService _db;

        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;

        public ObservableCollection<Category> Categories { get; } = new();

        public AddCategoryViewModel(IDatabaseService db)
        {
            _db = db;
            LoadCategoriesAsync();
        }

        [RelayCommand]
        private async Task LoadCategoriesAsync()
        {
            var list = await _db.GetCategoriesAsync();
            Categories.Clear();
            foreach (var cat in list)
                Categories.Add(cat);
        }

        [RelayCommand]
        private async Task SaveCategoryAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await App.Current.MainPage.DisplayAlert("Ошибка", "Введите название категории!", "OK");
                return;
            }

            var category = new Category
            {
                Name = Name.Trim(),
                Icon = string.IsNullOrWhiteSpace(Icon) ? "🏠" : Icon.Trim(),
                IsDefault = false
            };

            await _db.SaveCategoryAsync(category);
            await App.Current.MainPage.DisplayAlert("Сохранено", "Категория успешно добавлена!", "OK");

            // очистим поля
            Name = string.Empty;
            Icon = string.Empty;

            await LoadCategoriesAsync();
        }
    }
}
