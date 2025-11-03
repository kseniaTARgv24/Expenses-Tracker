using Expenses_Tracker.Views;
using Expenses_Tracker.ViewModels;
using Expenses_Tracker.Services;
using System.Globalization;


namespace Expenses_Tracker;

public partial class App : Application
{
    public App(MainViewModel mainVm)
    {
        InitializeComponent();

        // прочитать сохраненный язык (если есть)
        var saved = Preferences.Get("AppLanguage", null);
        if (!string.IsNullOrEmpty(saved))
        {
            try
            {
                LocalizationResourceManager.Instance.SetCulture(new CultureInfo(saved));
            }
            catch { }
        }

        LocalizationResourceManager.Instance.SetResourceManager(
        Expenses_Tracker.Resources.Localization.AppResources.ResourceManager
            );

        //MainPage = new NavigationPage(new Views.MainPage(mainVm));
        MainPage = new AppShell();
    }
}
