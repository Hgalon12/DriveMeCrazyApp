using DriveMeCrazyApp.ViewModels;

namespace DriveMeCrazyApp.Views;

public partial class AssignmentView : ContentPage
{
	public AssignmentView(AssignmentViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}