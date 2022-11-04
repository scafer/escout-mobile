using escout.Helpers;
using escout.Models.Database;
using escout.Models.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace escout.Services.Database
{
    public class Query
    {

        public async Task<DbAthlete> GetAthlete()
        {
            var athletes = await new Database<DbAthlete>().Select();
            return athletes.FirstOrDefault();
        }

        public async Task<List<DbGameEvent>> GetGameEvents()
        {
            return await new Database<DbGameEvent>().Select();
        }

        public async Task<int> GetEventId(string name)
        {
            var events = await new Database<DbEvent>().Select();
            var evt = events.FirstOrDefault(e => e.Name == name);
            return evt.Id;
        }

        public async Task<List<DbAthlete>> GetAthletes(int gameId, int clubId)
        {
            var athletes = await new Database<DbAthlete>().Select();
            return athletes.Where(x => x.DataExt == gameId && x.ClubId == clubId).ToList();
        }

        public async Task<List<DbClub>> GetClubs(int gameId)
        {
            var clubs = await new Database<DbClub>().Select();
            return clubs.Where(x => x.DataExt == gameId).ToList();
        }

        public async Task<List<DbEvent>> GetEvents()
        {
            return await new Database<DbEvent>().Select();
        }

        public async Task<List<DbEvent>> GetEvents(int dataExt)
        {
            var events = await new Database<DbEvent>().Select();
            return events.Where(x => x.DataExt == dataExt).ToList();
        }

        public async Task<DbClub> GetClub(int clubId)
        {
            var clubs = await new Database<DbClub>().Select();
            return clubs.Where(x => x.Id == clubId).FirstOrDefault();
        }

        public async Task<int> AddGameEvent(DbGameEvent gameEvent)
        {
            var gameEvents = new Database<DbGameEvent>();
            return await gameEvents.Insert(gameEvent);
        }

        public async void UpdateGameEventStatus(DbGameEvent gameEvent)
        {
            var gameEvents = new Database<DbGameEvent>();
            await gameEvents.Update(gameEvent);
        }

        public async Task<List<DbGame>> GetDbGame(int status)
        {
            var games = await new Database<DbGame>().Select();
            return games.Where(x => x.Status == status).ToList();
        }

        public async Task<List<DbGame>> GetDbGameById(int id)
        {
            var games = await new Database<DbGame>().Select();
            return games.Where(x => x.Id == id).ToList();
        }

        public async Task AddGameData(GameData gameData)
        {
            try
            {
                await AddGameToDb(gameData.Game);
                await AddSportToDb(gameData.Sport, gameData.Game.Id);
                await AddClubToDb(gameData.Clubs, gameData.Game.Id);
                await AddAthleteToDb(gameData.Athletes, gameData.Game.Id);
                await AddCompetitionToDb(gameData.Competition, gameData.Game.Id);
                await AddEventToDb(gameData.Events, gameData.Game.Id);
                await AddGameEventToDb(gameData.GameEvents);

                await Application.Current.MainPage.DisplayAlert(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_GAME_SAVE, ConstValues.OPTION_OK);
            }

            catch (Exception ex)
            {
                ExceptionHandler.GenericException(ex);
            }
        }

        private async Task AddGameToDb(Game game)
        {
            await new Database<DbGame>().Insert(new DbGame(game));
        }

        private async Task AddSportToDb(Sport sport, int gameId)
        {
            await new Database<DbSport>().Insert(new DbSport(sport, gameId));
        }

        private async Task AddClubToDb(List<Club> clubs, int gameId)
        {
            foreach (var club in clubs)
            {
                await new Database<DbClub>().Insert(new DbClub(club, gameId));
            }
        }

        private async Task AddAthleteToDb(List<Athlete> athletes, int gameId)
        {
            foreach (var athlete in athletes)
            {
                await new Database<DbAthlete>().Insert(new DbAthlete(athlete, gameId));
            }
        }

        private async Task AddCompetitionToDb(Competition competition, int gameId)
        {
            await new Database<DbCompetition>().Insert(new DbCompetition(competition, gameId));
        }

        private async Task AddEventToDb(List<Event> events, int gameId)
        {
            foreach (var e in events)
            {
                await new Database<DbEvent>().Insert(new DbEvent(e, gameId));
            }
        }

        private async Task AddGameEventToDb(List<GameEvent> gameEvents)
        {
            foreach (var e in gameEvents)
            {
                await new Database<DbGameEvent>().Insert(new DbGameEvent(e));
            }
        }

        public async Task RemoveGameData(int dataExt)
        {
            try
            {
                foreach (var e in await new Database<DbGame>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbGame>().Delete(e);
                    }
                }

                foreach (var e in await new Database<DbAthlete>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbAthlete>().Delete(e);
                    }
                }

                foreach (var e in await new Database<DbClub>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbClub>().Delete(e);
                    }
                }

                foreach (var e in await new Database<DbCompetition>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbCompetition>().Delete(e);
                    }
                }

                foreach (var e in await new Database<DbEvent>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbEvent>().Delete(e);
                    }
                }

                foreach (var e in await new Database<DbGameEvent>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbGameEvent>().Delete(e);
                    }
                }

                foreach (var e in await new Database<DbSport>().Select())
                {
                    if (e.DataExt == dataExt)
                    {
                        await new Database<DbSport>().Delete(e);
                    }
                }

                await Application.Current.MainPage.DisplayAlert(ConstValues.TITLE_STATUS_INFO, ConstValues.MSG_GAME_REMOVE, ConstValues.OPTION_OK);
            }

            catch (Exception ex)
            {
                ExceptionHandler.GenericException(ex);
            }
        }
    }
}
