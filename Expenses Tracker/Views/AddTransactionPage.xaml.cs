using Expenses_Tracker.Models;
using Expenses_Tracker.Resources;
using Expenses_Tracker.Services.Interfaces;
using Expenses_Tracker.ViewModels;

namespace Expenses_Tracker.Views;

public partial class AddTransactionPage : ContentPage
{
    private readonly IDatabaseService _db;

    public AddTransactionPage()
    {
        InitializeComponent();
        BindingContext = new AddTransactionViewModel(ServiceHelper.GetService<IDatabaseService>());
    }

}
