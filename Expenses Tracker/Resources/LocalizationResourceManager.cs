using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Expenses_Tracker.Resources
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        // Статическая ссылка на singleton
        private static LocalizationResourceManager _instance;
        public static LocalizationResourceManager Instance => _instance ??= new LocalizationResourceManager();

        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public event PropertyChangedEventHandler PropertyChanged;

        // Инициализация ResourceManager
        public void SetResourceManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            _currentCulture = CultureInfo.CurrentUICulture;
        }

        // Индексатор для привязки в XAML
        public string this[string text]
        {
            get
            {
                if (_resourceManager == null) return text;
                return _resourceManager.GetString(text, _currentCulture) ?? text;
            }
        }

        // Текущая культура
        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (_currentCulture == value) return;
                _currentCulture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
    }
}
