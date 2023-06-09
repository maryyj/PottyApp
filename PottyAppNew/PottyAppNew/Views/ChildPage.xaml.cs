using Plugin.Maui.Audio;
using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class ChildPage : ContentPage
{
    private readonly Delegates.MyDelegate _alertDelegate;
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
            _alertDelegate("Felmeddelande", "Du m�ste vara inloggad f�r att l�gga till ett barn");
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
            _alertDelegate("Felmeddelande", "Du m�ste l�gga till ett barn f�r att kunna l�gga till en h�ndelse.");
        }

    }

    private async void OnBackCLicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}