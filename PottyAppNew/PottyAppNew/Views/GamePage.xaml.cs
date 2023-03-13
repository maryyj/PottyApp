namespace PottyAppNew.Views;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
	}
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}