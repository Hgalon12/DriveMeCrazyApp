using DriveMeCrazyApp.Services;
using DriveMeCrazyApp.ViewModels;
using DriveMeCrazyApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace DriveMeCrazyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Karantina-Bold.ttf", "KarantinaBold");
                    fonts.AddFont("Karantina-Light.ttf", "KarantinaLight");
                    fonts.AddFont("Karantina-Regular.ttf", "KarantinaRegular");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<AppShell>();
            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DriveMeCrazyWebAPIProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ViewModelBase>();
            return builder;
        }
        
    }
}
