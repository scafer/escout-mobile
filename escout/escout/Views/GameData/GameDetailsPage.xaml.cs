using escout.Helpers;
using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.GameData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameDetailsPage : ContentPage
    {
        private Game game;

        private SQLiteAsyncConnection _connection;

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

                    _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                    
                    await _connection.CreateTableAsync<DbAthlete>();
                    await _connection.CreateTableAsync<DbClub>();
                    await _connection.CreateTableAsync<DbCompetition>();
                    await _connection.CreateTableAsync<DbEvent>();
                    await _connection.CreateTableAsync<DbGame>();
                    await _connection.CreateTableAsync<DbGameEvent>(); 
                    await _connection.CreateTableAsync<DbSport>();

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
            var obj = new DbGame(game);
            await _connection.InsertAsync(obj);
        }

        private async Task AddSportToDb(Sport sport, int gameId)
        {
            var obj = new DbSport(sport, gameId);
            await _connection.InsertAsync(obj);
        }

        private async Task AddClubToDb(List<Club> clubs, int gameId)
        {
            foreach (var club in clubs)
            {
                var obj = new DbClub(club, gameId);
                await _connection.InsertAsync(obj);
            }
        }

        private async Task AddAthleteToDb(List<Athlete> athletes, int gameId)
        {
            foreach (var athlete in athletes)
            {
                var obj = new DbAthlete(athlete, gameId);
                await _connection.InsertAsync(obj);
            }
        }

        private async Task AddCompetitionToDb(Competition competition, int gameId)
        {
            var obj = new DbCompetition(competition, gameId);
            await _connection.InsertAsync(obj);
        }

        private async Task AddEventToDb(List<Event> evts, int gameId)
        {
            foreach (var e in evts)
            {
                var obj = new DbEvent(e, gameId);
                await _connection.InsertAsync(obj);
            }
        }

        private async Task AddGameEventToDb(List<GameEvent> gameEvents)
        {
            foreach (var e in gameEvents)
            {
                var obj = new DbGameEvent(e);
                await _connection.InsertAsync(obj);
            }
        }
    }
}