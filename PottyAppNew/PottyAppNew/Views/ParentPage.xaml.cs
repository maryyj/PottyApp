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
            _alertDelegate("Felmeddelande", "Du m�ste vara inloggad/l�gga till ett barn f�r att l�gga till en h�ndelse.");
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
            _alertDelegate("Error", "Du m�ste vara inloggad/l�gga till ett barn f�r att se din statistik");
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
            _alertDelegate("Error", "Du m�ste vara inloggad f�r att l�gga till ett barn");
        }
    }
    private async void OnBackCklicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}