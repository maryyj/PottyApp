using PottyAppNew.Helpers;
using PottyAppNew.ViewModels;

namespace PottyAppNew.Views;

public partial class AddChildPage : ContentPage
{
    AddChildPageViewModel addChildPageViewModel = new();
    private Delegates.MyDelegate _alertDelegate;
    public AddChildPage()
    {
        InitializeComponent();
        BindingContext = addChildPageViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
        addChildPageViewModel.GetChildren();
    }
    private async void OnClickedSaveChild(object sender, EventArgs e)
    {
        bool success = await addChildPageViewModel.AddChildToDatabase();
        if (success)
        {
            _alertDelegate("Sparad", "Ditt barn har lagts till.");
            nameInput.Text = String.Empty;
            ageInput.Text = String.Empty;
        }
        else
        {
            _alertDelegate("Felmeddelande", "Fel inmatning. Ditt barn sparades inte, försök igen.");
        }
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    //private async void OnListViewItemSelected(object sender, EventArgs e)
    //{
    //    var children = ((ListView)sender).SelectedItem as Models.Child;
    //    if (children != null && listOfChildren.SelectedItem != null)
    //    {
    //        var page = new AddChildPage();
    //        page.BindingContext = children;
    //        await Navigation.PushAsync(page);
    //        listOfChildren.SelectedItem = null;
    //    }
    //}

}