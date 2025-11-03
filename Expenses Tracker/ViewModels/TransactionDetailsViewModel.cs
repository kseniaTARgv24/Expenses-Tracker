using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Expenses_Tracker.Helpers;
using Expenses_Tracker.Models;
using Expenses_Tracker.Resources;
using Expenses_Tracker.Services.Interfaces;
using Microsoft.Maui.Controls;

namespace Expenses_Tracker.ViewModels
{
    public partial class TransactionDetailsViewModel : ObservableObject
    {
        public Transaction Transaction { get; }
        private readonly IDatabaseService _db;

        [ObservableProperty]
        private Category category;

        public TransactionDetailsViewModel(Transaction transaction)
        {
            Transaction = transaction;
            _db = ServiceHelper.GetService<IDatabaseService>(); 
            _ = LoadCategoryAsync();
        }


        private async Task LoadCategoryAsync()
        {
            if (Transaction.CategoryId > 0)
                Category = await _db.GetCategoryByIdAsync(Transaction.CategoryId);
        }

        public double Amount => Transaction.Amount;
        public string? Note => Transaction.Note;
        public DateTime Date => Transaction.Date;
        public TransactionType Type => Transaction.Type;
        public bool IsExpense => Type == TransactionType.Expense;

        public string CategoryDisplay =>
            Category != null ? (Category.Icon + " " + Category.Name) : "—";
        partial void OnCategoryChanged(Category value)
        {
            OnPropertyChanged(nameof(CategoryDisplay));
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

