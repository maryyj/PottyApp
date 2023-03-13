using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PottyAppNew.Views;

public partial class InfoPage : ContentPage
{

    public InfoPage()
    {
        InitializeComponent();
    }
    private async void OnClickedGoTo1177Web(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://www.1177.se/Sormland/barn--gravid/att-ta-hand-om-barn/praktiska-rad-i-vardagen/att-borja-ga-pa-pottan-eller-toaletten/#section-39137"));
    }
    private async void OnClickedGoToRullaVagnWeb(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://rullavagn.nu/artikel/pottraning-eller-sluta-med-bloja-ultimata-guiden-med-noll-stress-till-att-bli-blojfri/"));
    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickedGoToPampersWeb(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://www.pampers.se/smabarn/pottraning/artikel/steg-foer-steg-goer-pottraeningen-rolig"));
    }
}
