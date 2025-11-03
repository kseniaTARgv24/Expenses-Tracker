namespace Expenses_Tracker.Helpers;

public static class ServiceHelper
{
    public static T GetService<T>() => Current.GetService<T>();
    public static IServiceProvider Current =>
        IPlatformApplication.Current.Services;
}
