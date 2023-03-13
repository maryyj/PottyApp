using PottyAppNew.Models;

namespace PottyAppNew;

public partial class App : Application
{
    public static Parent LoggedInParent { get; set; }
    public static Child Child { get; set; }
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
