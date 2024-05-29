namespace AmirGarciaAppMaui.Views;

public partial class AGAboutPage : ContentPage
{
	public AGAboutPage()
	{
		InitializeComponent();
	}

    private async void AgLearnMore_Clicked(object sender, EventArgs e)
    {
        await Launcher.Default.OpenAsync("https://aka.ms/maui");
    }
}