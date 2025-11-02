using Expenses_Tracker.Views;
using Expenses_Tracker.ViewModels;


namespace Expenses_Tracker;

public partial class App : Application
{
    public App(MainViewModel mainVm)
    {
        InitializeComponent();
        MainPage = new NavigationPage(new Views.MainPage(mainVm));
    }
}
