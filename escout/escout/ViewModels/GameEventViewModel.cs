using escout.Helpers;
using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System.ComponentModel;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class GameEventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;

        public GameEventViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }

        public async void RegisterEvent(DbGameEvent dbGameEvent)
        {

            try
            {
                var db = new LocalDb();
                dbGameEvent.Syncronized = false;
                dbGameEvent.Id = await db.AddGameEvent(dbGameEvent);

                var gameEvent = new GameEvent()
                {
                    AthleteId = dbGameEvent.AthleteId,
                    EventDescription = dbGameEvent.EventDescription,
                    EventId = dbGameEvent.EventId,
                    GameId = dbGameEvent.GameId,
                    GameTime = dbGameEvent.GameTime,
                    Time = dbGameEvent.GameTime,
                    Key = dbGameEvent.Key,
                    UserId = dbGameEvent.UserId
                };

                var response = await RestConnector.PostObjectAsync(RestConnector.GameEvent, gameEvent);
                var result = JsonConvert.DeserializeObject<SvcResult>(response);

                if (result.ErrorCode == 0)
                {
                    dbGameEvent.Syncronized = true;
                    _ = db.UpdateGameEventStatus(dbGameEvent);
                }
            }
            catch { }
        }
    }
}
