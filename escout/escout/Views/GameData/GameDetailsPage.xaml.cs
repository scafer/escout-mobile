using escout.Helpers;
using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameDetailsPage : ContentPage
    {
        private Game game;

        public GameDetailsPage(Game game)
        {
            InitializeComponent();
            BindingContext = game;
            this.game = game;
        }

        private async void AddGameToWatchList(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Message", "Add to watch list?", "Ok", "Cancel");

            if (response)
            {
                await SaveGameData(game.Id);
            }
        }

        private async Task SaveGameData(int gameId)
        {
            var response = await RestConnector.GetObjectAsync(RestConnector.GameData + "?gameId=" + gameId);
            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    var gameData = JsonConvert.DeserializeObject<Models.Rest.GameData>(response);

                    await AddGameToDb(gameData.game);
                    await AddSportToDb(gameData.sport, gameData.game.Id);
                    await AddClubToDb(gameData.clubs, gameData.game.Id);
                    await AddAthleteToDb(gameData.athletes, gameData.game.Id);
                    await AddCompetitionToDb(gameData.competition, gameData.game.Id);
                    await AddEventToDb(gameData.events, gameData.game.Id);
                    await AddGameEventToDb(gameData.gameEvents);
                }
                catch (Exception e)
                {
                    await DisplayAlert("Error", e.ToString(), "Ok");
                }
            }
        }

        private async Task AddGameToDb(Game game)
        {
            using Database<DbGame> database = new Database<DbGame>();
            await database.Insert(new DbGame(game));
        }

        private async Task AddSportToDb(Sport sport, int gameId)
        {
            using Database<DbSport> database = new Database<DbSport>();
            await database.Insert(new DbSport(sport, gameId));
        }

        private async Task AddClubToDb(List<Club> clubs, int gameId)
        {
            using Database<DbClub> database = new Database<DbClub>();
            foreach (var club in clubs)
            {
                await database.Insert(new DbClub(club, gameId));
            }
        }

        private async Task AddAthleteToDb(List<Athlete> athletes, int gameId)
        {
            using Database<DbAthlete> database = new Database<DbAthlete>();
            foreach (var athlete in athletes)
            {
                await database.Insert(new DbAthlete(athlete, gameId));
            }
        }

        private async Task AddCompetitionToDb(Competition competition, int gameId)
        {
            using Database<DbCompetition> database = new Database<DbCompetition>();
            await database.Insert(new DbCompetition(competition, gameId));
        }

        private async Task AddEventToDb(List<Event> evts, int gameId)
        {
            using Database<DbEvent> database = new Database<DbEvent>();
            foreach (var e in evts)
            {
                await database.Insert(new DbEvent(e, gameId));
            }
        }

        private async Task AddGameEventToDb(List<GameEvent> gameEvents)
        {
            using Database<DbGameEvent> database = new Database<DbGameEvent>();
            foreach (var e in gameEvents)
            {
                await database.Insert(new DbGameEvent(e));
            }
        }
    }
}