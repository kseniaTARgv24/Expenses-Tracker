using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Expenses_Tracker.Models;
using Expenses_Tracker.Resources;
using Expenses_Tracker.Services;
using Expenses_Tracker.Services.Interfaces;
using Expenses_Tracker.Views;
using System.Collections.ObjectModel;



namespace Expenses_Tracker.ViewModels
{
    public partial class MainViewModel : ObservableObject // A base class for objects of which the properties must be observable. using CommunityToolkit.Mvvm.ComponentModel;
                                                          //It is needed so that the View (interface) is automatically updated when the ViewModel changes its data.
    {                                                       //https://stackoverflow.com/questions/3601901/when-is-it-appropriate-to-use-c-sharp-partial-classes
        private readonly IDatabaseService _db;



        [ObservableProperty]
        private string todayText = DateTime.Now.ToString("dd MM yyyy",  LocalizationResourceManager.Instance.CurrentCulture); //public string ToString([StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string? format)

        [ObservableProperty]
        private string currentMonth = DateTime.Now.ToString("MMMM yyyy", LocalizationResourceManager.Instance.CurrentCulture);  //grapths current month

        [ObservableProperty]
        private double totalIncome;  //grapths current month

        [ObservableProperty]
        private double totalExpense;  //grapths current month

        [ObservableProperty]
        private ObservableCollection<CategoryExpense> categoryExpenses = new(); //grapths current month
        public ObservableCollection<TransactionViewItem> Transactions { get; } = new();

        public MainViewModel(IDatabaseService db)
        {
            _db = db;
            SettingsService.OnCurrencyChanged += (s, e) => UpdateCurrency();
        }

        [RelayCommand]   //using CommunityToolkit.Mvvm.Input;
        public async Task LoadAsync()
        {
            var now = DateTime.Now;

            var monthTx = await _db.GetTransactionsByMounthAsync(now.Year, now.Month); //from DatabaseService

            var allCats = await _db.GetCategoriesAsync();

            Transactions.Clear();
            foreach (var t in monthTx)
            {
                var category = allCats.FirstOrDefault(c => c.Id == t.CategoryId);
                Transactions.Add(new TransactionViewItem(t, category));
            }

            TotalIncome = monthTx
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            TotalExpense = monthTx
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            var groups = monthTx
                .Where(t => t.Type == TransactionType.Expense)
                .GroupBy(t => t.CategoryId)
                .Select(g => new CategoryExpense
                {
                    CategoryId = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToList();

            foreach (var g in groups)
            {
                var cat = allCats.FirstOrDefault(c => c.Id == g.CategoryId);
                g.Icon = cat?.Icon ?? "❔";
            }

            CategoryExpenses.Clear();
            foreach (var g in groups)
                CategoryExpenses.Add(g);
        }

        [RelayCommand]
        public async Task DeleteAsync(Transaction t)
        {
            await _db.DeleteTransactionAsync(t);
            await LoadAsync();
        }

        [RelayCommand]
        public async Task ShowDetailsAsync(Transaction transaction)
        {
            if (transaction == null)
                return;

            Console.WriteLine("Show details ");
            await App.Current.MainPage.Navigation.PushAsync(new TransactionDetailsPage
            {
                BindingContext = new TransactionDetailsViewModel(transaction)
            });
        }

        /////////////////////////////////

        public class CategoryExpense  //grapths current month
        {
            public int CategoryId { get; set; }
            public string Icon { get; set; }
            public double Total { get; set; }
        }

        /////////////////////////////////

        [ObservableProperty]
        private string totalIncomeFormatted;

        partial void OnTotalIncomeChanged(double value) => TotalIncomeFormatted = $"{SettingsService.CurrencySymbol}{value:N2}";

        [ObservableProperty]
        private string totalExpenseFormatted;

        partial void OnTotalExpenseChanged(double value) => TotalExpenseFormatted = $"{SettingsService.CurrencySymbol}{value:N2}";


        private void UpdateCurrency()
        {
            OnPropertyChanged(nameof(TotalIncomeFormatted));
            OnPropertyChanged(nameof(TotalExpenseFormatted));
        }

    }
}
