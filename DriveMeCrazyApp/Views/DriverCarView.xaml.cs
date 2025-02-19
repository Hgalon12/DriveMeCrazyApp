using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class DriverCarView : ContentPage
{
	public DriverCarView(DriverCarViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}