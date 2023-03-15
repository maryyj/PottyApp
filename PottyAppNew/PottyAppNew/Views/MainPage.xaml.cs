using PottyAppNew.Helpers;
using PottyAppNew.ViewModels;
using PottyAppNew.Views;

namespace PottyAppNew;

public partial class MainPage : ContentPage
{
    MainPageViewModel mainPageViewModel = new MainPageViewModel();
    private Delegates.MyDelegate _alertDelegate;
    public MainPage()
    {
        InitializeComponent();
        BindingContext = mainPageViewModel;
        _alertDelegate = Delegates.DisplayAlerts;
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
            emailEntry.Text = String.Empty;
            passwordEntry.Text = String.Empty;
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            //alert ange något i fälten.
            _alertDelegate("Felmeddelande", "Du måste fylla i emailadress och lösenord för att logga in.");
        }
    }

    //private async void OnClickedGoDirectlyToHomePage(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new HomePage());
    //}
}

