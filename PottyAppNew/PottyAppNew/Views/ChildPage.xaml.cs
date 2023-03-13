using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class ChildPage : ContentPage
{
    private Delegates.MyDelegate _alertDelegate;
    public ChildPage()
    {
        InitializeComponent();
        _alertDelegate = Delegates.DisplayAlerts;
    }

    private async void OnClickedGoToAddChildPage(object sender, EventArgs e)
    {
        if (App.LoggedInParent != null)
        {

            await Navigation.PushAsync(new AddChildPage());
        }
        else
        {
            _alertDelegate("Error", "Du måste vara inloggad för att lägga till ett barn");
        }
    }
    private async void OnClickedGoToVideoPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VideoPage());
    }

    private async void OnClickedGoToGamePage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GamePage());
    }

    private async void OnClickedGoToChildEventPage(object sender, EventArgs e)
    {
        if (App.LoggedInParent != null && App.Child != null)
        {

            await Navigation.PushAsync(new ChildEventPage());
        }
        else
        {
            _alertDelegate("Error", "Du måste vara inloggad/lägga till ett barn för att kunna lägga till en händelse.");
        }

    }

    private async void OnBackCLicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}