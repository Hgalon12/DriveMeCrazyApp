using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class ApproveRequestView : ContentPage
{
	public ApproveRequestView( AprroveRequestViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}