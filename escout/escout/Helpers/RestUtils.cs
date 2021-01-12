using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public static class RestUtils
    {
        public static async Task<Image> GetImage(int? imageId)
        {
            var img = new Image();
            var response = await RestConnector.GetObjectAsync(RestConnector.IMAGE + "?id=" + imageId);
            if (HttpStatusCode.OK.Equals(RestConnector.GetStatusCode(response)))
            {
                img = JsonConvert.DeserializeObject<Image>(await RestConnector.GetContent(response));
            }

            return img;
        }

        public static async Task<bool> AddGameUser(int gameId, int athleteId)
        {
            try
            {
                var gameUsers = new List<GameUser> { new(int.Parse(App.UserId), gameId, athleteId) };
                var response = await RestConnector.PostObjectAsync(RestConnector.GAME_USER, gameUsers);
                return !string.IsNullOrEmpty(await RestConnector.GetContent(response));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static async Task<Club> GetClub(int clubId)
        {
            var club = new Club();
            var request = RestConnector.CLUB + "?id=" + clubId;

            var response = await RestConnector.GetObjectAsync(request);
            if (!string.IsNullOrEmpty(await RestConnector.GetContent(response)))
                club = JsonConvert.DeserializeObject<Club>(await RestConnector.GetContent(response));

            return club;
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
