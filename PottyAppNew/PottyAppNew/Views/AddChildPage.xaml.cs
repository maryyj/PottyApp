using PottyAppNew.Helpers;
using PottyAppNew.ViewModels;

namespace PottyAppNew.Views;

public partial class AddChildPage : ContentPage
{
    AddChildPageViewModel childPageViewModel = new();
    private Delegates.MyDelegate _alertDelegate;
    public AddChildPage()
    {
        InitializeComponent();
        BindingContext = childPageViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
        childPageViewModel.GetChildren();
    }
    private async void OnClickedGoToChildPage(object sender, EventArgs e)
    {
        bool success = await childPageViewModel.AddChildToDatabase();
        if (success)
        {
            _alertDelegate("Sparad", "Ditt barn har lagts till.");
            nameInput.Text = String.Empty;
            ageInput.Text = String.Empty;
        }
    }
    private async void OnListViewItemSelected(object sender, EventArgs e)
    {
        var children = ((ListView)sender).SelectedItem as Models.Child;
        if (children != null && listOfChildren.SelectedItem != null)
        {
            var page = new AddChildPage();
            page.BindingContext = children;
            await Navigation.PushAsync(page);
            listOfChildren.SelectedItem = null;
        }
    }
}