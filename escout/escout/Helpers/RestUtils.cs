using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public static class RestUtils
    {

        public static async Task<Image> GetImage(int? imageId)
        {
            var img = new Image();
            var response = await RestConnector.GetObjectAsync(RestConnector.IMAGE + "?id=" + imageId);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
            {
                img = JsonConvert.DeserializeObject<Image>(await RestConnector.GetContent(response));
            }

            return img;
        }

        public static async Task<bool> AddGameUser(int gameId, int athleteId)
        {
            try
            {
                List<GameUser> gameUsers = new List<GameUser>();
                gameUsers.Add(new GameUser(int.Parse(App.UserId), gameId, athleteId));
                var response = await RestConnector.PostObjectAsync(RestConnector.GAME_USER, gameUsers);
                if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static async Task<Club> GetClub(int clubId)
        {
            var _club = new Club();
            var request = RestConnector.CLUB + "?id=" + clubId;

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
                _club = JsonConvert.DeserializeObject<Club>(await RestConnector.GetContent(response));

            return _club;
        }

        public static async Task<List<CompetitionBoard>> GetCompetitionBoardById(int id)
        {
            var board = new List<CompetitionBoard>();
            var request = RestConnector.COMPETITION_BOARD + "?id=" + id;

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
                board = JsonConvert.DeserializeObject<List<CompetitionBoard>>(await RestConnector.GetContent(response));

            return board;
        }

        public static async Task UpdateGameStatus(int gameId, int status)
        {
            try
            {
                var gameRequest = RestConnector.GAME + "?id=" + gameId;

                var gameResponse = await RestConnector.GetObjectAsync(gameRequest);
                if (!string.IsNullOrEmpty(await RestConnector.GetContent(gameResponse)))
                {
                    var game = JsonConvert.DeserializeObject<Game>(await RestConnector.GetContent(gameResponse));
                    game.Status = status;
                    await RestConnector.PutObjectAsync(RestConnector.GAME, game);
                }
            }
            catch (Exception ex)
            {
                await App.DisplayMessage(Message.TITLE_STATUS_ERROR, ex.Message, Message.OPTION_OK);
            }
        }
    }
}
