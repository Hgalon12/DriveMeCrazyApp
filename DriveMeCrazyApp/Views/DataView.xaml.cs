using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class DataView : ContentPage
{
	public DataView(DataViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}