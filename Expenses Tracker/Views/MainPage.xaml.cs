using Expenses_Tracker.ViewModels;
using System.Transactions;

namespace Expenses_Tracker.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTransactionPage());
    }

    private async void OnAddCatClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCategoryPage());
    }



}
