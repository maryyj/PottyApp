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
        base.OnAppearing();
        listOfStatistic.ItemsSource = await StatisticPageViewModel.GetStatistic(App.Child);

    }
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}