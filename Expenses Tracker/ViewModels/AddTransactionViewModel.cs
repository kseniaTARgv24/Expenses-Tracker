using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Expenses_Tracker.Models;
using Expenses_Tracker.Services.Interfaces;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Expenses_Tracker.ViewModels
{
    public partial class AddTransactionViewModel : ObservableObject
    {
        private readonly IDatabaseService _db;

        [ObservableProperty]
        private double amount;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private TransactionType type = TransactionType.Expense;

        [ObservableProperty]
        private string note;

        [ObservableProperty]
        private Category selectedCategory;
        public ObservableCollection<Category> Categories { get; set; } = new();


        public ObservableCollection<TransactionType> TransactionTypes { get; } =
        new ObservableCollection<TransactionType>(Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>());


        public AddTransactionViewModel(IDatabaseService db)
        {
            _db = db;
            LoadCategoriesAsync();
        }

        public bool IsExpense => Type == TransactionType.Expense;

        partial void OnTypeChanged(TransactionType value)
        {
            OnPropertyChanged(nameof(IsExpense));
        }

        private async void LoadCategoriesAsync()
        {
            var list = await _db.GetCategoriesAsync();

            if (list.Count == 0)
            {
                var home = new Category { Name = "Home", Icon = "🏠", IsDefault = true };
                await _db.SaveCategoryAsync(home);
                list = await _db.GetCategoriesAsync();
            }

            Categories.Clear();
            foreach (var c in list)
                Categories.Add(c);

        }

        [RelayCommand]
        public async Task SaveTransactionAsync()
        {
            if (amount <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Заполните правильное количество!", "OK");
                return;
            }
            if (SelectedCategory == null && IsExpense)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка", "Категория не выбрана!", "OK");
                return;
            }

            var transaction = new Transaction
            {
                Amount = amount, 
                Type = Type,
                CategoryId = SelectedCategory?.Id ?? 0,
                Date = Date,
                Note = Note
            };

            if (Type == TransactionType.Expense && SelectedCategory != null)
                transaction.CategoryId = SelectedCategory.Id;

            await _db.SaveTransactionAsync(transaction);

            await App.Current.MainPage.DisplayAlert("Saved", "Транзакция успешно сохранена!", "OK");
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
