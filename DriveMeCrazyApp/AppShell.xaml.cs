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
            Routing.RegisterRoute("approveRequest", typeof(ApproveRequestView));
            Routing.RegisterRoute("requestCar", typeof(RequestCarView));

        }
        public event Action<Type> DataChanged;
        public void Refresh(Type type)
        {
            if (DataChanged != null)
            {
                DataChanged(type);
            }
        }
    }
}
