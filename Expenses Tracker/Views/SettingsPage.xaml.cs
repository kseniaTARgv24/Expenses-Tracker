using System.Globalization;
using Expenses_Tracker.Services;
using Expenses_Tracker.Resources;

namespace Expenses_Tracker.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        ThemePicker.SelectedIndex = App.Current.RequestedTheme == AppTheme.Dark ? 1 : 0;
        CurrencyPicker.SelectedIndex = SettingsService.CurrencySymbol switch
        {
            "$" => 0,
            "€" => 1,
            "₽" => 2,
            _ => 0
        };

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


    private void CurrencyPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = CurrencyPicker.SelectedItem?.ToString();

        if (selected.Contains("$")) SettingsService.CurrencySymbol = "$";
        else if (selected.Contains("€")) SettingsService.CurrencySymbol = "€";
        else if (selected.Contains("₽")) SettingsService.CurrencySymbol = "₽";
    }


}
