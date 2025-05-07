using DriveMeCrazyApp.Models;
using DriveMeCrazyApp.Views;

namespace DriveMeCrazyApp
{
    public partial class App : Application
    {
        public TableUser? LoggedInUser { get; set; }
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            LoginView? v = serviceProvider.GetService<LoginView>();
            DataView d = serviceProvider.GetService<DataView>();
            MainPage = new NavigationPage(v);
        }
    }
}
