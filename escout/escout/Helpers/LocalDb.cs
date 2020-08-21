using escout.Helpers.Services;
using escout.Models.Db;
using escout.Models.Rest;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace escout.Helpers
{
    public class LocalDb
    {
        private readonly SQLiteAsyncConnection connection;

        public LocalDb() => connection = DependencyService.Get<ISqLiteDb>().GetConnection();

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

        public async Task<DbAthlete> GetAthlete()
        {
            await InitializeDb();
            return await connection.Table<DbAthlete>().FirstOrDefaultAsync();
        }

        public async Task<List<DbGameEvent>> GetGameEvents()
        {
            await InitializeDb();
            return await connection.Table<DbGameEvent>().ToListAsync();
        }

        public async Task<int> GetEventId(string name)
        {
            await InitializeDb();
            var evt = await connection.Table<DbEvent>().FirstOrDefaultAsync(e => e.Name == name);
            return evt.Id;
        }

        public async Task<List<DbAthlete>> GetAthletes(int gameId, int clubId)
        {
            await InitializeDb();
            return await connection.Table<DbAthlete>().Where(x => x.DataExt == gameId && x.ClubId == clubId).ToListAsync();
        }

        public async Task<List<DbClub>> GetClubs(int gameId)
        {
            await InitializeDb();
            return await connection.Table<DbClub>().Where(x => x.DataExt == gameId).ToListAsync();
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

        public async Task<List<DbGame>> GetDbGame(int status)
        {
            await InitializeDb();
            var games = new List<DbGame>();

            foreach (var game in await connection.Table<DbGame>().ToListAsync())
                if (game.Status == status)
                    games.Add(game);

            return games;
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

                await Application.Current.MainPage.DisplayAlert(Message.TITLE_STATUS_RESULT, Message.GAME_SAVE, Message.OPTION_OK);
            }

            catch (Exception ex) { ExceptionHandler.GenericException(ex); }
        }

        public async Task RemoveGameData(int dataExt)
        {
            await InitializeDb();

            try
            {
                foreach (var e in await connection.Table<DbGame>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                foreach (var e in await connection.Table<DbAthlete>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                foreach (var e in await connection.Table<DbClub>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                foreach (var e in await connection.Table<DbCompetition>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                foreach (var e in await connection.Table<DbEvent>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                foreach (var e in await connection.Table<DbGameEvent>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                foreach (var e in await connection.Table<DbSport>().ToListAsync())
                    if (e.DataExt == dataExt)
                        await connection.DeleteAsync(e);

                await Application.Current.MainPage.DisplayAlert(Message.TITLE_STATUS_RESULT, Message.GAME_REMOVE, Message.OPTION_OK);
            }

            catch (Exception ex) { ExceptionHandler.GenericException(ex); }
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

        private async Task AddEventToDb(List<Event> events, int gameId)
        {
            foreach (var e in events)
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
