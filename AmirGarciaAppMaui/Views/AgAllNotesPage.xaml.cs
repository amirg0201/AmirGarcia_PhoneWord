namespace AmirGarciaAppMaui.Views;

public partial class AgAllNotesPage : ContentPage
{
	public AgAllNotesPage()
	{
		InitializeComponent();

        BindingContext = new Models.AgAllNotes();
    }

    protected override void OnAppearing()
    {
        ((Models.AgAllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AgNotePage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.AgNotes)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(AgNotePage)}?{nameof(AgNotePage.ItemId)}={note.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}