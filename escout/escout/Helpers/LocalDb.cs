using escout.Models.Db;
using escout.Models.Rest;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace escout.Helpers
{
    public class LocalDb //TODO: improve this
    {
        private readonly SQLiteAsyncConnection connection;

        public LocalDb() => connection = DependencyService.Get<ISqLiteDb>().GetConnection();


        public async Task<List<DbGame>> GetDbGame(int status)
        {
            await InitializeDb();
            var games = new List<DbGame>();

            foreach (var game in await connection.Table<DbGame>().ToListAsync())
            {
                if (game.Status == status)
                    games.Add(game);
            }

            return games;
        }

        public async Task<DbAthlete> GetAthlete()
        {
            await InitializeDb();
            return await connection.Table<DbAthlete>().FirstOrDefaultAsync();
        }

        public async Task<int> GetEventId(string name)
        {
            await InitializeDb();
            var e = await connection.Table<DbEvent>().FirstOrDefaultAsync(x => x.Name == name);
            return e.Id;
        }

        public async Task AddGameData(GameData gameData)
        {
            await InitializeDb();
            try
            {
                await AddGameToDb(gameData.game);
                await AddSportToDb(gameData.sport, gameData.game.Id);
                await AddClubToDb(gameData.clubs, gameData.game.Id);
                await AddAthleteToDb(gameData.athletes, gameData.game.Id);
                await AddCompetitionToDb(gameData.competition, gameData.game.Id);
                await AddEventToDb(gameData.events, gameData.game.Id);
                await AddGameEventToDb(gameData.gameEvents);

                await Application.Current.MainPage.DisplayAlert("Message", "Game was successfully saved.", "OK");
            }
            catch (Exception ex)
            {
                ExceptionHandler.GenericException(ex);
            }
        }

        public async Task RemoveGameData(int dataExt)
        {
            await InitializeDb();

            try
            {
                foreach (var e in await connection.Table<DbGame>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }

                foreach (var e in await connection.Table<DbAthlete>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }

                foreach (var e in await connection.Table<DbClub>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }
                foreach (var e in await connection.Table<DbCompetition>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }
                foreach (var e in await connection.Table<DbEvent>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }
                foreach (var e in await connection.Table<DbGameEvent>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }
                foreach (var e in await connection.Table<DbSport>().ToListAsync())
                {
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);
                }

                await Application.Current.MainPage.DisplayAlert("Message", "Game was successfully removed.", "OK");
            }
            catch (Exception ex)
            {
                ExceptionHandler.GenericException(ex);
            }
        }

        public async Task<int> AddGameEvent(DbGameEvent gameEvent)
        {
            await InitializeDb();
            return await connection.InsertAsync(gameEvent);
        }

        public async Task UpdateGameEventStatus(DbGameEvent gameEvent)
        {
            await InitializeDb();
            await connection.UpdateAsync(gameEvent);
        }

        private async Task InitializeDb()
        {
            await connection.CreateTableAsync<DbAthlete>();
            await connection.CreateTableAsync<DbClub>();
            await connection.CreateTableAsync<DbCompetition>();
            await connection.CreateTableAsync<DbEvent>();
            await connection.CreateTableAsync<DbGame>();
            await connection.CreateTableAsync<DbGameEvent>();
            await connection.CreateTableAsync<DbSport>();
        }

        private async Task AddGameToDb(Game game)
        {
            var obj = new DbGame(game);
            await connection.InsertAsync(obj);
        }

        private async Task AddSportToDb(Sport sport, int gameId)
        {
            var obj = new DbSport(sport, gameId);
            await connection.InsertAsync(obj);
        }

        private async Task AddClubToDb(List<Club> clubs, int gameId)
        {
            foreach (var club in clubs)
            {
                var obj = new DbClub(club, gameId);
                await connection.InsertAsync(obj);
            }
        }

        private async Task AddAthleteToDb(List<Athlete> athletes, int gameId)
        {
            foreach (var athlete in athletes)
            {
                var obj = new DbAthlete(athlete, gameId);
                await connection.InsertAsync(obj);
            }
        }

        private async Task AddCompetitionToDb(Competition competition, int gameId)
        {
            var obj = new DbCompetition(competition, gameId);
            await connection.InsertAsync(obj);
        }

        private async Task AddEventToDb(List<Event> evts, int gameId)
        {
            foreach (var e in evts)
            {
                var obj = new DbEvent(e, gameId);
                await connection.InsertAsync(obj);
            }
        }

        private async Task AddGameEventToDb(List<GameEvent> gameEvents)
        {
            foreach (var e in gameEvents)
            {
                var obj = new DbGameEvent(e);
                await connection.InsertAsync(obj);
            }
        }
    }
}
