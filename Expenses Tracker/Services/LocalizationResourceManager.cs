using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Microsoft.Maui.Storage; // Для Preferences

namespace Expenses_Tracker.Services
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        private static LocalizationResourceManager _instance;
        public static LocalizationResourceManager Instance => _instance ??= new LocalizationResourceManager();

        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler LanguageChanged;

        private LocalizationResourceManager() { }

        public void SetResourceManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;

            // Загружаем сохранённый язык или системный
            var savedLang = Preferences.Get("AppLanguage", CultureInfo.CurrentUICulture.Name);
            _currentCulture = new CultureInfo(savedLang);

            CultureInfo.DefaultThreadCurrentCulture = _currentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = _currentCulture;
        }


        public string this[string text]
        {
            get
            {
                if (_resourceManager == null) return text;
                return _resourceManager.GetString(text, _currentCulture) ?? text;
            }
        }


        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (value == null || _currentCulture?.Name == value.Name) return;

                _currentCulture = value;

                CultureInfo.DefaultThreadCurrentCulture = value;
                CultureInfo.DefaultThreadCurrentUICulture = value;

                Preferences.Set("AppLanguage", value.Name);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
                LanguageChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public void SetCulture(CultureInfo ci) => CurrentCulture = ci;
    }
}
