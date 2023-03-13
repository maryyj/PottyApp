using PottyAppNew.ViewModels;

namespace PottyAppNew.Views;

public partial class VideoPage : ContentPage
{

    VideoPageViewModel viewModel = new VideoPageViewModel();
    public VideoPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        string videoId1 = "KN84YXb4_wc";
        string videoId2 = "MzIrrkctWRI";
        string videoId3 = "tAMSnbTbF58";
        base.OnAppearing();

        // Kallar på GetVideoAsync metoden att ladda filmerna
        MySongText.Text = "Sånger";
        await viewModel.GetVideoAsync(mediaElement1, videoId1);
        await viewModel.GetVideoAsync(mediaElement2, videoId2);
        MyMovieText.Text = "Korta Filmer";
        await viewModel.GetVideoAsync(mediaElement3, videoId3);

    }
}