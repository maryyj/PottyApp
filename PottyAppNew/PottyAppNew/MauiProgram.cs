using CommunityToolkit.Maui;
using Plugin.Maui.Audio;
using PottyAppNew.Views;

namespace PottyAppNew;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //Audiomanager som kan kan skapa flera spelare i appen.
        builder.Services.AddSingleton(AudioManager.Current);

        return builder.Build();
    }
}
