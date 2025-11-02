using Expenses_Tracker.Resources;
using System.Globalization;

namespace Expenses_Tracker.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        // Инициализация выбранных значений
        ThemePicker.SelectedIndex = App.Current.RequestedTheme == AppTheme.Dark ? 1 : 0;
        LanguagePicker.SelectedIndex = 0; // по умолчанию English
    }

    private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = ThemePicker.SelectedItem.ToString();
        if (selected == "Light")
            App.Current.UserAppTheme = AppTheme.Light;
        else
            App.Current.UserAppTheme = AppTheme.Dark;
    }

    private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = LanguagePicker.SelectedItem.ToString();
        switch (selected)
        {
            case "English":
                LocalizationResourceManager.Instance.CurrentCulture = new CultureInfo("en");
                break;
            case "Русский":
                LocalizationResourceManager.Instance.CurrentCulture = new CultureInfo("ru");
                break;
            case "Eesti":
                LocalizationResourceManager.Instance.CurrentCulture = new CultureInfo("et");
                break;
        }
    }

}
