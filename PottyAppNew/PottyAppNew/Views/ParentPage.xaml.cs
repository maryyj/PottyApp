using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class ParentPage : ContentPage
{
    private readonly Delegates.MyDelegate _alertDelegate;
    public ParentPage()
    {
        InitializeComponent();
        _alertDelegate = Delegates.DisplayAlerts;
    }
    private async void OnClickedGoToInfoPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }
    private async void OnClickedGoToParentEventPage(object sender, EventArgs e)
    {
        if (App.LoggedInParent != null && App.Child != null)
        {
            await Navigation.PushAsync(new ParentEventPage());
        }
        else
        {
            _alertDelegate("Felmeddelande", "Du måste vara inloggad/lägga till ett barn för att lägga till en händelse.");
        }
    }
    private async void OnClickedGoStatisticPage(object sender, EventArgs e)
    {
        if (App.LoggedInParent != null && App.Child != null)
        {
            await Navigation.PushAsync(new StatisticPage());
        }
        else
        {
            _alertDelegate("Error", "Du måste vara inloggad/lägga till ett barn för att se din statistik");
        }
    }
    private async void OnClickedGoToDadJokesPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DadJokesPage());
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
    private async void OnBackCklicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}