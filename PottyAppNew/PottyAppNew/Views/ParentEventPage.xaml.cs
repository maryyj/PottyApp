using PottyAppNew.Helpers;
using PottyAppNew.ViewModels;

namespace PottyAppNew.Views;
public partial class ParentEventPage : ContentPage
{
    //Frame p� bilderna.
    readonly EventPageViewModel eventPageViewModel = new();
    private readonly Delegates.MyDelegate _alertDelegate;
    public ParentEventPage()
    {
        InitializeComponent();
        BindingContext = eventPageViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
    }
    private async void OnClickedAddEventDescription(object sender, EventArgs e)
    {
        //L�gger till en timme i tiden f�r att det ska bli r�tt tid i databasen. DateTime.Now k�r sommartid
        DateTime date = DateTime.Now.AddHours(1);

        if (sender == PoopEvent) eventPageViewModel.EventDescription = "Bajsat i pottan";
        else if (sender == UrineEvent) eventPageViewModel.EventDescription = "Kissat i pottan";
        else if (sender == EatEvent) eventPageViewModel.EventDescription = "�tit mat";
        else if (sender == DrinkEvent) eventPageViewModel.EventDescription = "Druckit";
        else if (sender == DryNightEvent) eventPageViewModel.EventDescription = "Torr bl�ja hela natten";
        else if (sender == AccidentEvent) eventPageViewModel.EventDescription = "Olycka: Kissat/bajsat p� sig";

        // L�gger till event i databasen, skickar med datum, beskrivning och ett Barn
        bool isSavedInDb = await eventPageViewModel.AddEventToDatabase(date, eventPageViewModel.EventDescription, App.Child);

        if (isSavedInDb)
        {
            _alertDelegate("Sparad", "H�ndelsen �r sparad");
        }
        else
        {
            _alertDelegate("Felmeddelande", "Gick inte att spara h�ndelse, f�rs�k igen.");
        }
    }
    private async void OnClickedGoToStatisticPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatisticPage());
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}