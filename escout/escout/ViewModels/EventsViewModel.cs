using escout.Models.Db;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using escout.Helpers;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class EventsViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SyncronizeEventsCommand;

        private ObservableCollection<DbGameEvent> gameEvents;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EventsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SyncronizeEventsCommand = new Command(SyncronizeEventsExecuted);

            _ = LoadEvents();
        }

        public ObservableCollection<DbGameEvent> GameEvents
        {
            get => gameEvents;
            set
            {
                gameEvents = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadEvents()
        {
            LocalDb db = new LocalDb();
            GameEvents = new ObservableCollection<DbGameEvent>(await db.GetGameEvents());
        }

        private async void SyncronizeEventsExecuted()
        {

        }
    }
}
