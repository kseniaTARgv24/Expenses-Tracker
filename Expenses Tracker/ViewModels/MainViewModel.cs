using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Expenses_Tracker.Services.Interfaces;
using System.Collections.ObjectModel;
using Expenses_Tracker.Models;
using Expenses_Tracker.Views;


namespace Expenses_Tracker.ViewModels
{
    public partial class MainViewModel : ObservableObject // A base class for objects of which the properties must be observable. using CommunityToolkit.Mvvm.ComponentModel;
                                                          //It is needed so that the View (interface) is automatically updated when the ViewModel changes its data.
    {                                                       //https://stackoverflow.com/questions/3601901/when-is-it-appropriate-to-use-c-sharp-partial-classes
        private readonly IDatabaseService _db;



        [ObservableProperty]
        private string todayText = DateTime.Now.ToString("dd MM yyyy"); //public string ToString([StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string? format)

        public ObservableCollection<TransactionViewItem> Transactions { get; } = new();

        public MainViewModel(IDatabaseService db)
        {
            _db = db;
        }

        [RelayCommand]   //using CommunityToolkit.Mvvm.Input;
        public async Task LoadAsync()
        {
            var transactions = await _db.GetTransactionsAsync();
            Transactions.Clear();

            foreach (var t in transactions)
            {
                Category cat = null;
                if (t.Type == TransactionType.Expense && t.CategoryId > 0)
                {
                    cat = await _db.GetCategoryByIdAsync(t.CategoryId);
                }

                Transactions.Add(new TransactionViewItem(t, cat));
            }

            [RelayCommand]
            async Task DeleteAsync(Transaction t)
            {
                await _db.DeleteTransactionAsync(t);
                await LoadAsync();
            }

            [RelayCommand]
            async Task ShowDetailsAsync(Transaction transaction)
            {
                if (transaction == null)
                    return;


                await App.Current.MainPage.Navigation.PushAsync(new TransactionDetailsPage
                {
                    BindingContext = new TransactionDetailsViewModel(transaction)
                });
            }


        }
    } 
}
