using PottyAppNew.ViewModels;
using PottyAppNew.Helpers;

namespace PottyAppNew.Views;

public partial class StatisticPage : ContentPage
{
    //TODO: Visa statistic som finns i Eventcollection.
    StatisticPageViewModel statViewModel = new StatisticPageViewModel();
    private Delegates.MyDelegate _alertDelegate;
    public StatisticPage()
    {
        InitializeComponent();
        BindingContext = statViewModel;
    }
    protected override async void OnAppearing()
    {
        string peePotty = "Kissat i pottan";
        string poopPotty = "Bajsat i pottan";
        string accident = "Olycka: Kissat/bajsat på sig";
        string dryDiaper = "Torr blöja hela natten";
        base.OnAppearing();
        listOfStatisticPotty.ItemsSource = await StatisticPageViewModel.GetStatisticTwoEvent(App.Child, peePotty, poopPotty);
        listOfStatisticAccident.ItemsSource = await StatisticPageViewModel.GetStatistic(App.Child,accident);
        listOfStatisticDryDiaper.ItemsSource = await StatisticPageViewModel.GetStatistic(App.Child,dryDiaper);

    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}