using PottyAppNew.ViewModels;
using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class VideoPage : ContentPage
{
    private readonly Delegates.MyDelegate _alertDelegate;
    public VideoPage()
    {
        InitializeComponent();
        _alertDelegate = Delegates.DisplayAlerts;
    }
    protected override async void OnAppearing()
    {
        string videoId1 = "KN84YXb4_wc";
        string videoId2 = "MzIrrkctWRI";
        string videoId3 = "KuYfCrb4brY";
        string videoId4 = "tAMSnbTbF58";
        base.OnAppearing();

        // Kallar på GetVideoAsync metoden att ladda filmerna
        try
        {

            MySongText.Text = "Sånger";
            await VideoPageViewModel.GetVideoAsync(mediaElement1, videoId1);
            await VideoPageViewModel.GetVideoAsync(mediaElement2, videoId2);
            await VideoPageViewModel.GetVideoAsync(mediaElement3, videoId3);
            MyMovieText.Text = "Korta Filmer";
            await VideoPageViewModel.GetVideoAsync(mediaElement4, videoId4);
        }
        catch
        {
            _alertDelegate("Felmeddelande", "Videon gick inte att hämta, försök igen senare.");
        }

    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}