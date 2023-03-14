using PottyAppNew.ViewModels;
using MongoDB.Bson;
using PottyAppNew.Helpers;
using Microsoft.Maui;
using Plugin.Maui.Audio;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PottyAppNew.Views;

public partial class ChildEventPage : ContentPage
{
    private Delegates.MyDelegate _alertDelegate;
    private readonly IAudioManager _audioManager; //Inbyggd Interface i audiomanager
    EventPageViewModel eventPageViewModel = new EventPageViewModel();
    public ChildEventPage()
    {
        InitializeComponent();
        BindingContext = eventPageViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
        this._audioManager = new AudioManager(); //Gör att man kan använda audiomanager utanför konstruktor.
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetGoldenStars();
    }
    private void GetGoldenStars()
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
        string poopPotty = "Bajsat i pottan";
        string peePotty = "Kissat i pottan";

        if (sender == PoopEvent)
        {
            eventPageViewModel.EventDescription = poopPotty;
        }
        else if (sender == UrineEvent)
        {
            eventPageViewModel.EventDescription = peePotty;
        }
        //Lägger till händelsen i databasen
        bool isSavedInDb = await eventPageViewModel.AddEventToDatabase(date, eventPageViewModel.EventDescription, App.Child);

        if (isSavedInDb)
        {
            //lägger till en stjärna direkt på sidan.
            Image pointImage = new Image();
            pointImage.Source = "star.png";
            stackLayout.Children.Add(pointImage);

            var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("fart.mp3"));
            player.Play();

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