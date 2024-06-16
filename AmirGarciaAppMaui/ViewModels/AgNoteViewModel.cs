using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace AmirGarciaAppMaui.ViewModels
{


    internal class AgNoteViewModel : ObservableObject, IQueryAttributable
    {
        private Models.AgNotes _note;


        public string Text
        {
            get => _note.AgText;
            set
            {
                if (_note.AgText != value)
                {
                    _note.AgText = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime AgDate => _note.AgDate;

        public string AgIdentifier => _note.AgFilename;
        public ICommand AgSaveCommand { get; private set; }
        public ICommand AgDeleteCommand { get; private set; }

        public AgNoteViewModel()
        {
            _note = new Models.AgNotes();
            AgSaveCommand = new AsyncRelayCommand(AgSave);
            AgDeleteCommand = new AsyncRelayCommand(AgDelete);
        }

        public AgNoteViewModel(Models.AgNotes note)
        {
            _note = note;
            AgSaveCommand = new AsyncRelayCommand(AgSave);
            AgDeleteCommand = new AsyncRelayCommand(AgDelete);
        }

        private async Task AgSave()
        {
            _note.AgDate = DateTime.Now;
            _note.AgSave();
            await Shell.Current.GoToAsync($"..?saved={_note.AgFilename}");
        }

        private async Task AgDelete()
        {
            _note.AgDelete();
            await Shell.Current.GoToAsync($"..?deleted={_note.AgFilename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Models.AgNotes.AgLoad(query["load"].ToString());
                AgRefreshProperties();
            }
        }

        public void AgReload()
        {
            _note = Models.AgNotes.AgLoad(_note.AgFilename);
            AgRefreshProperties();
        }

        private void AgRefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(AgDate));
        }
    }
}
