using PottyAppNew.ViewModels;
using MongoDB.Bson;
using PottyAppNew.Helpers;
using Microsoft.Maui;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PottyAppNew.Views;

public partial class ChildEventPage : ContentPage
{
    private Delegates.MyDelegate _alertDelegate;

    EventPageViewModel eventPageViewModel = new EventPageViewModel();
    public ChildEventPage()
    {
        InitializeComponent();
        BindingContext = eventPageViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetGoldenStars();
    }
    private async void GetGoldenStars()
    {
        if (App.Child != null)
        {
            if (stackLayout != null)
            {
                //Skapar en bild för varje poäng barnet har, och lägger till det i stackLayouten.
                for (int i = 0; i < App.Child.Points; i++)
                {
                    Image pointImage = new Image();
                    pointImage.Source = "star.png";
                    stackLayout.Children.Add(pointImage);
                }
            }
        }
        else
        {
            _alertDelegate("Error", "Lägg till ett barn först.");
        }
    }
    private async void OnClickedAddEventDescription(object sender, EventArgs e)
    {
        DateTime date = DateTime.Now.AddHours(1);

        if (sender == PoopEvent)
        {
            eventPageViewModel.EventDescription = "Bajsat i pottan";
        }
        else if (sender == UrineEvent)
        {
            eventPageViewModel.EventDescription = "Kissat i pottan";
        }

        bool isSavedInDb = await eventPageViewModel.AddEventToDatabase(date, eventPageViewModel.EventDescription, App.Child);

        if (isSavedInDb)
        {
            Image pointImage = new Image();
            pointImage.Source = "star.png";
            stackLayout.Children.Add(pointImage);

            _alertDelegate("Sparad", "Händelsen är sparad");
        }
        else
        {
            _alertDelegate("Error", "Gick inte att spara händelse, försök igen.");
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