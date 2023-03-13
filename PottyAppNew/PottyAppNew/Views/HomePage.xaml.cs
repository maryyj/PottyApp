namespace PottyAppNew.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
    private async void OnClickedGoToChildPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChildPage());
    }

    private async void OnClickedGoToParentPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ParentPage());
    }
}