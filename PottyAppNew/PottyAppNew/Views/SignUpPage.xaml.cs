using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class SignUpPage : ContentPage
{
    readonly ViewModels.SignUpPageViewModel signUpPage = new();
    private readonly Delegates.MyDelegate _alertDelegate;
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
            _alertDelegate("Grattis", "Du är nu registrerad, vänligen logga in.");
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            _alertDelegate("Felmeddelande", "Fel inmatning, försök igen.");
        }
    }
}