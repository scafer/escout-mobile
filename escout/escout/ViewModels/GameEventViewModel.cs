using escout.Helpers;
using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace escout.ViewModels
{
    class GameEventViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameEventViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public static async Task RegisterEvent(DbGameEvent dbGameEvent)
        {
            try
            {
                var db = new LocalDb();
                dbGameEvent.Synchronized = false;
                dbGameEvent.Id = await db.AddGameEvent(dbGameEvent);

                var gameEvent = new GameEvent()
                {
                    AthleteId = dbGameEvent.AthleteId,
                    ClubId = dbGameEvent.ClubId,
                    EventDescription = dbGameEvent.EventDescription,
                    EventId = dbGameEvent.EventId,
                    GameId = dbGameEvent.GameId,
                    GameTime = dbGameEvent.GameTime,
                    Time = dbGameEvent.Time,
                    Key = dbGameEvent.Key,
                    UserId = dbGameEvent.UserId,
                };

                var gameEvents = new List<GameEvent> { gameEvent };
                var response = await RestConnector.PostObjectAsync(RestConnector.GAME_EVENT, gameEvents);
                var result = JsonConvert.DeserializeObject<SvcResult>(await RestConnector.GetContent(response));

                if (result.ErrorCode == 0)
                {
                    dbGameEvent.Synchronized = true;
                    _ = db.UpdateGameEventStatus(dbGameEvent);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
