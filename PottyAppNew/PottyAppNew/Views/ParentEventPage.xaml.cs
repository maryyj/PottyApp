using PottyAppNew.Helpers;
using PottyAppNew.ViewModels;

namespace PottyAppNew.Views;
public partial class ParentEventPage : ContentPage
{
    //Frame på bilderna.
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
        //Lägger till en timme i tiden för att det ska bli rätt tid i databasen. DateTime.Now kör sommartid
        DateTime date = DateTime.Now.AddHours(1);

        if (sender == PoopEvent) eventPageViewModel.EventDescription = "Bajsat i pottan";
        else if (sender == UrineEvent) eventPageViewModel.EventDescription = "Kissat i pottan";
        else if (sender == EatEvent) eventPageViewModel.EventDescription = "Ätit mat";
        else if (sender == DrinkEvent) eventPageViewModel.EventDescription = "Druckit";
        else if (sender == DryNightEvent) eventPageViewModel.EventDescription = "Torr blöja hela natten";
        else if (sender == AccidentEvent) eventPageViewModel.EventDescription = "Olycka: Kissat/bajsat på sig";

        // Lägger till event i databasen, skickar med datum, beskrivning och ett Barn
        bool isSavedInDb = await eventPageViewModel.AddEventToDatabase(date, eventPageViewModel.EventDescription, App.Child);

        if (isSavedInDb)
        {
            _alertDelegate("Sparad", "Händelsen är sparad");
        }
        else
        {
            _alertDelegate("Felmeddelande", "Gick inte att spara händelse, försök igen.");
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