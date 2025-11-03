namespace Expenses_Tracker.Services;

public static class SettingsService
{
    private const string CurrencyKey = "CurrencySymbol";
    private static string _currencySymbol = Preferences.Get(CurrencyKey, "$");

    public static string CurrencySymbol
    {
        get => _currencySymbol;
        set
        {
            _currencySymbol = value;
            Preferences.Set(CurrencyKey, value);
            OnCurrencyChanged?.Invoke(null, EventArgs.Empty);
        }
    }

    public static event EventHandler OnCurrencyChanged;
}
