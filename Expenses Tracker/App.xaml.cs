using Expenses_Tracker.Views;
using Expenses_Tracker.ViewModels;
using Expenses_Tracker.Resources;


namespace Expenses_Tracker;

public partial class App : Application
{
    public App(MainViewModel mainVm)
    {
        InitializeComponent();

        LocalizationResourceManager.Instance.SetResourceManager(
        Expenses_Tracker.Resources.Localization.AppResources.ResourceManager
            );

        MainPage = new NavigationPage(new Views.MainPage(mainVm));
    }
}
