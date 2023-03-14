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
        this._audioManager = new AudioManager(); //G�r att man kan anv�nda audiomanager utanf�r konstruktor.
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
                //Skapar en bild f�r varje po�ng barnet har, och l�gger till det i stackLayouten.
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
            _alertDelegate("Error", "L�gg till ett barn f�rst.");
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
        //L�gger till h�ndelsen i databasen
        bool isSavedInDb = await eventPageViewModel.AddEventToDatabase(date, eventPageViewModel.EventDescription, App.Child);

        if (isSavedInDb)
        {
            //l�gger till en stj�rna direkt p� sidan.
            Image pointImage = new Image();
            pointImage.Source = "star.png";
            stackLayout.Children.Add(pointImage);

            var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("fart.mp3"));
            player.Play();

            _alertDelegate("Sparad", "H�ndelsen �r sparad");
        }
        else
        {
            _alertDelegate("Error", "Gick inte att spara h�ndelse, f�rs�k igen.");
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