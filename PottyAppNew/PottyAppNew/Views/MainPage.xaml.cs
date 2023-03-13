using PottyAppNew.ViewModels;
using PottyAppNew.Views;

namespace PottyAppNew;

public partial class MainPage : ContentPage
{
    MainPageViewModel mainPageViewModel = new MainPageViewModel();
    public MainPage()
    {
        InitializeComponent();
        BindingContext = mainPageViewModel;
    }

    private async void OnSignUpLabelClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

    private async void OnClickedGoToHomePage(object sender, EventArgs e)
    {
        bool success = await mainPageViewModel.CheckUserExists();
        if (success)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }

    private async void OnClickedGoDirectlyToHomePage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
}

