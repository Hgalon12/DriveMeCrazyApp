using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}