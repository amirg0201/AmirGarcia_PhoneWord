namespace AmirGarciaAppMaui.Views;

public partial class AGAboutPage : ContentPage
{
	public AGAboutPage()
	{
		InitializeComponent();
	}

    private async void AgLearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.AgAbout about)
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
        }
    }
}