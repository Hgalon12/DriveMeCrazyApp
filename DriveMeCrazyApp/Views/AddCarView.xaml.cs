using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class AddCarView : ContentPage
{
	public AddCarView(AddCarViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}