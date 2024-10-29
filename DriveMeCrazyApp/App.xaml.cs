using DriveMeCrazyApp.Models;

namespace DriveMeCrazyApp
{
    public partial class App : Application
    {
        public TableUser? LoggedInUser { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
