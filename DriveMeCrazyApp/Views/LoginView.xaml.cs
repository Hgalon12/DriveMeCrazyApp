using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}

    private void Label_SizeChanged(object sender, EventArgs e)
    {

    }
}