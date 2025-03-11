using DriveMeCrazyApp.ViewModels;
using DriveMeCrazyApp.Views;

namespace DriveMeCrazyApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("addCar", typeof(AddCarView));
            Routing.RegisterRoute("addDriver", typeof(DriverCarView));

        }
    }
}
