using PottyAppNew.Models;
using PottyAppNew.ViewModels;

namespace PottyAppNew.Views;

public partial class DadJokesPage : ContentPage
{
    //TODO: Översätt joke till svenska med hjälp av chat gpt api.
    DadJokesViewModel dadJokesViewModel = new();
    public DadJokesPage()
    {
        InitializeComponent();
        BindingContext = dadJokesViewModel;
    }

    private async void OnClickedGetDadJoke(object sender, EventArgs e)
    {
        string uri = "/v1/dadjokes?limit=10";
        List<DadJoke> jokes = await DadJokesViewModel.GetJokesAsync(uri);
        if (jokes != null && jokes.Count > 0)
        {
            Random random = new Random();
            int index = random.Next(jokes.Count);
            textSwe.Text = "Svenska:";
            dadJokeSwe.Text = await DadJokesViewModel.ChatTranslate(jokes[index].Joke);
            textEng.Text = "Makes no sense? Engelska originalversionen:";
            dadJokeEng.Text = jokes[index].Joke;
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}