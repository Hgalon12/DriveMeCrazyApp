using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}