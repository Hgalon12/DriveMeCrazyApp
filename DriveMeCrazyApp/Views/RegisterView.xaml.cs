using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class RegisterView : ContentView
{
	public RegisterView(RegisterViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}