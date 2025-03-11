using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class CarListView : ContentPage
{
	public CarListView(CarListViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}