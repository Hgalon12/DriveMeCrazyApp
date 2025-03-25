using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class CalenderView : ContentPage
{
	public CalenderView(CalendarViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}