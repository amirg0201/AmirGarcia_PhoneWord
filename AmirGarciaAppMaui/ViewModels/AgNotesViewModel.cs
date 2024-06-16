using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AmirGarciaAppMaui.ViewModels
{
    internal class NotesViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.AgNoteViewModel> AgAllNotes { get; }
        public ICommand AgNewCommand { get; }
        public ICommand AgSelectNoteCommand { get; }

        public NotesViewModel()
        {
            AgAllNotes = new ObservableCollection<ViewModels.AgNoteViewModel>(Models.AgNotes.LoadAll().Select(n => new AgNoteViewModel(n)));
            AgNewCommand = new AsyncRelayCommand(AgNewNoteAsync);
            AgSelectNoteCommand = new AsyncRelayCommand<ViewModels.AgNoteViewModel>(AgSelectNoteAsync);
        }

        private async Task AgNewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.AgNotePage));
        }

        private async Task AgSelectNoteAsync(ViewModels.AgNoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.AgNotePage)}?load={note.AgIdentifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                AgNoteViewModel matchedNote = AgAllNotes.Where((n) => n.AgIdentifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    AgAllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                AgNoteViewModel matchedNote = AgAllNotes.Where((n) => n.AgIdentifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.AgReload();
                    AgAllNotes.Move(AgAllNotes.IndexOf(matchedNote), 0);
                }
                // If note isn't found, it's new; add it.
                else
                    AgAllNotes.Insert(0, new AgNoteViewModel(Models.AgNotes.AgLoad(noteId)));
            }
        }
    }
}
