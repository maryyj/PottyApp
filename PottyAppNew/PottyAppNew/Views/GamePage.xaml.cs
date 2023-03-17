using Plugin.Maui.Audio;

namespace PottyAppNew.Views;

public partial class GamePage : ContentPage
{
    private readonly IAudioManager _audioManager = new AudioManager(); //Inbyggd Interface i audiomanager
    public GamePage()
    {
        InitializeComponent();
        //_audioManager = new AudioManager();
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void SoundPlayer(string soundFile)
    {
        IAudioPlayer player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(soundFile));
        player.Play();
    }

    private void OnClickedPlaySound(object sender, EventArgs e)
    {
        if (sender == pooImg) SoundPlayer("caca.mp3");
        else if (sender == peeImg) SoundPlayer("orina.mp3");
        else if (sender == toiletImg) SoundPlayer("indoro.mp3");
        else if (sender == toothBrushImg) SoundPlayer("cepillo_de_dientes.mp3");
        else if (sender == toothpasteImg) SoundPlayer("pasta_de_diente.mp3");
        else if (sender == showerImg) SoundPlayer("ducha.mp3");
        else if (sender == strawberryImg) SoundPlayer("fresa.mp3");
        else if (sender == appleImg) SoundPlayer("manzana.mp3");
        else if (sender == icecreamImg) SoundPlayer("helado.mp3");
        else if (sender == catImg) SoundPlayer("gato.mp3");
        else if (sender == dogImg) SoundPlayer("perro.mp3");
        else if (sender == horseImg) SoundPlayer("caballo.mp3");
        else if (sender == carImg) SoundPlayer("coche.mp3");
        else if (sender == trainImg) SoundPlayer("tren.mp3");
        else if (sender == busImg) SoundPlayer("autobus.mp3");
    }
}