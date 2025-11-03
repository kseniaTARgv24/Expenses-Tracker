using System.Globalization;
using Expenses_Tracker.Services;

namespace Expenses_Tracker.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        // Инициализация выбранных значений
        /*ThemePicker.SelectedIndex = App.Current.RequestedTheme == AppTheme.Dark ? 1 : 0;
        LanguagePicker.SelectedIndex = 0; // по умолчанию English*/
    }

    private void ThemePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = ThemePicker.SelectedItem.ToString();
        if (selected == "Light")
            App.Current.UserAppTheme = AppTheme.Light;
        else
            App.Current.UserAppTheme = AppTheme.Dark;
    }

    private void ApplyLanguage(string lang)
    {
        var ci = new CultureInfo(lang);
        LocalizationResourceManager.Instance.SetCulture(ci);
    }

    private void OnRussianClicked(object sender, EventArgs e)
    {
        ApplyLanguage("ru");
        DisplayAlert("OK", "Русский язык применен", "OK");
    }

    private void OnEnglishClicked(object sender, EventArgs e)
    {
        ApplyLanguage("en");
        DisplayAlert("OK", "English language applied", "OK");
    }


    /*private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
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
        }
    }*/

}
