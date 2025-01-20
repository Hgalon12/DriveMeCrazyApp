using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class RequestCarView : ContentPage
{
	public RequestCarView( RequestCarViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}