using Expenses_Tracker.Services.Interfaces;
using Expenses_Tracker.ViewModels;

namespace Expenses_Tracker.Views;

public partial class AddCategoryPage : ContentPage
{
	public AddCategoryPage()
	{
		InitializeComponent();
        BindingContext = new AddCategoryViewModel(ServiceHelper.GetService<IDatabaseService>());
    }
}