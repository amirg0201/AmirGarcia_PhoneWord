namespace AmirGarciaAppMaui.Views;

public partial class AgAllNotesPage : ContentPage
{
	public AgAllNotesPage()
	{
		InitializeComponent();

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}