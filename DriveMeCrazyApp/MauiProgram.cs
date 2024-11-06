using DriveMeCrazyApp.Services;
using DriveMeCrazyApp.ViewModels;
using DriveMeCrazyApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using CommunityToolkit.Maui;
namespace DriveMeCrazyApp
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
                    fonts.AddFont("Karantina-Bold.ttf", "KarantinaBold");
                    fonts.AddFont("Karantina-Light.ttf", "KarantinaLight");
                    fonts.AddFont("Karantina-Regular.ttf", "KarantinaRegular");
                    fonts.AddFont("Bebas-Regular.ttf", "Bebas");
                    fonts.AddFont("Hatolie.ttf", "Hatolie");
                    fonts.AddFont("JumperPERSONALUSEONLY-Extra-Bold.ttf", "JumperBold");
                    fonts.AddFont("JumperPERSONALUSEONLY-Regular.ttf", "JumperRegulr");
                    fonts.AddFont("SPEEDRACE PERSONAL USE ONLY!.ttf", "SPEEDRACE");

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
            builder.Services.AddTransient<RegisterView>();
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
            builder.Services.AddTransient<RegisterViewModel>();
            return builder;
        }
        
    }
}
