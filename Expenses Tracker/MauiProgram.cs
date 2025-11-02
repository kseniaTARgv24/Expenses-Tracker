using CommunityToolkit.Maui;
using Expenses_Tracker.Services;
using Expenses_Tracker.Services.Interfaces;
using Expenses_Tracker.ViewModels;
using Expenses_Tracker.Views;
using Microsoft.Extensions.Logging;

namespace Expenses_Tracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "expences.bd3");
            builder.Services.AddSingleton<IDatabaseService>(_ => new DatabaseService(dbPath));

            //// ThemeService, LocalizationService регистрируй аналогично:
            //builder.Services.AddSingleton<ThemeService>();
            //builder.Services.AddSingleton<LocalizationService>();

            //// ViewModels / Pages
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<App>();



#endif

            return builder.Build();
        }
    }
}
