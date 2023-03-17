using PottyAppNew.Models;
using PottyAppNew.ViewModels;
using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class DadJokesPage : ContentPage
{
    readonly DadJokesViewModel dadJokesViewModel = new();
    private readonly Delegates.MyDelegate _alertDelegate;
    public DadJokesPage()
    {
        InitializeComponent();
        BindingContext = dadJokesViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
    }

    private async void OnClickedGetDadJoke(object sender, EventArgs e)
    {
        string uri = "/v1/dadjokes?limit=10";
        List<DadJoke> jokes = await DadJokesViewModel.GetJokesAsync(uri);
        if (jokes != null && jokes.Count > 0)
        {
            Random random = new();
            int index = random.Next(jokes.Count);
            string randomisedJoke = jokes[index].Joke;

            try
            {
                string translatedText = await DadJokesViewModel.ChatTranslate(jokes[index].Joke);
                textSwe.Text = "Svenska:";
                dadJokeSwe.Text = translatedText;
                textEng.Text = "Makes no sense? Engelska originalversionen:";
                dadJokeEng.Text = randomisedJoke;
            }
            catch (Exception)
            {
                _alertDelegate("Felmeddelande", "Skämtet gick inte att hämta, försök igen.");
            }
        }
        else
        {
            _alertDelegate("Felmeddelande", "Skämtet gick inte att hämta, försök igen.");
        }

    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
