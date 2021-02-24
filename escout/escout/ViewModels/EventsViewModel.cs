using escout.Helpers;
using escout.Models.Database;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class EventsViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand SynchronizeEventsCommand { get; set; }

        private bool isBusy;
        private ObservableCollection<DbGameEvent> gameEvents;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EventsViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SynchronizeEventsCommand = new Command(SynchronizeEventsExecuted);
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

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get => !IsBusy;
            set
            {
                IsBusy = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadEvents()
        {
            var db = new LocalDb();
            GameEvents = new ObservableCollection<DbGameEvent>(await db.GetGameEvents());
        }

        private async void SynchronizeEventsExecuted()
        {
            var db = new LocalDb();
            var unsynchronizedEvents = GameEvents.Where(e => e.Synchronized).ToList();

            if (unsynchronizedEvents.Count != 0)
            {
                try
                {
                    IsVisible = true;
                    var response = await RestConnector.PostObjectAsync(RestConnector.GAME_EVENT, unsynchronizedEvents);

                    if (200 == (int)response.StatusCode)
                    {
                        foreach (var e in unsynchronizedEvents)
                        {
                            e.Synchronized = true;
                            db.UpdateGameEventStatus(e);
                        }
                        _ = LoadEvents();
                        await App.DisplayMessage(Message.TITLE_STATUS_INFO, Message.MSG_EVENTS_SYNCRONIZED, Message.OPTION_OK);
                    }
                }
                catch (Exception ex) { await App.DisplayMessage(Message.TITLE_STATUS_INFO, ex.Message, Message.OPTION_OK); }
                finally { IsVisible = false; }
            }
        }
    }
}
