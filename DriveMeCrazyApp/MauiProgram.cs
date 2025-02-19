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
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<AssignmentView>();
            builder.Services.AddTransient<RequestCarView>();
            builder.Services.AddTransient<AddCarView>();
            builder.Services.AddTransient<HighScoresView>();
            builder.Services.AddTransient<CalenderView>();
            builder.Services.AddTransient<DataView>();
            builder.Services.AddTransient<ApproveRequestView>();
            builder.Services.AddTransient<DriverCarView>();
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
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<AssignmentViewModel>();
            builder.Services.AddTransient<RequestCarViewModel>();
            builder.Services.AddTransient<AddCarViewModel>();
            builder.Services.AddTransient<HighScoresViewModel>();
            builder.Services.AddTransient<CalendarViewModel>();
            builder.Services.AddTransient<DataViewModel>();
            builder.Services.AddTransient<AprroveRequestViewModel>();
            builder.Services.AddTransient<DriverCarViewModel>();

            return builder;
        }
        
    }
}
