using escout.Helpers;
using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        private async Task LoadEvents()
        {
            var db = new LocalDb();
            GameEvents = new ObservableCollection<DbGameEvent>(await db.GetGameEvents());
        }

        private async void SynchronizeEventsExecuted()
        {
            var db = new LocalDb();
            var unsynchronizedEvents = GameEvents.Where(e => e.Synchronized == false).ToList();

            if (unsynchronizedEvents.Count != 0)
            {
                try
                {
                    var response = await RestConnector.PostObjectAsync(RestConnector.GameEvent, unsynchronizedEvents);
                    var result = JsonConvert.DeserializeObject<SvcResult>(response);

                    if (result.ErrorCode == 0)
                    {
                        foreach (var e in unsynchronizedEvents)
                        {
                            e.Synchronized = true;
                            _ = db.UpdateGameEventStatus(e);
                        }
                        _ = LoadEvents();
                        await App.DisplayMessage("Result", "Success", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    await App.DisplayMessage("Result", ex.Message, "Ok");
                }
            }
        }
    }
}
