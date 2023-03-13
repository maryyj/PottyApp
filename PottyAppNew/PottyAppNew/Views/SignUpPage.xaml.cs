using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class SignUpPage : ContentPage
{
    ViewModels.SignUpPageViewModel signUpPage = new();
    private Delegates.MyDelegate _alertDelegate;
    public SignUpPage()
    {
        InitializeComponent();
        BindingContext = signUpPage;
        _alertDelegate = Delegates.DisplayAlerts;
    }
    private async void OnClickedGoToMainPage(object sender, EventArgs e)
    {

        bool success = await signUpPage.AddParentToDatabase();
        if (success)
        {
            _alertDelegate("Grattis", "Du �r nu registrerad, v�nligen logga in.");
            await Navigation.PushAsync(new MainPage());
        }
    }
}