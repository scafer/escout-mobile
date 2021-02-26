using escout.Models.Database;
using escout.Models.Rest;
using escout.Services.Database;
using escout.Services.Rest;
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
                var db = new Query();
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

                if (200 == (int)response.StatusCode)
                {
                    dbGameEvent.Synchronized = true;
                    db.UpdateGameEventStatus(dbGameEvent);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
