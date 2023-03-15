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

    private async void OnLogOutClicked(object sender, EventArgs e)
    {
        App.LoggedInParent = null;
        App.Child = null;
        await Navigation.PopAsync();
    }
}