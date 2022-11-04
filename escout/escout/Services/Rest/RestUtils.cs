using escout.Models.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace escout.Services.Rest
{
    public static class RestUtils
    {

        public static async Task<Image> GetImage(int? imageId)
        {
            var img = new Image();
            var response = await RestConnector.GetObjectAsync(RestConnector.IMAGE + "?id=" + imageId);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                img = JsonConvert.DeserializeObject<Image>(await response.Content.ReadAsStringAsync());
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

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

            if (response.StatusCode == HttpStatusCode.OK)
            {
                club = JsonConvert.DeserializeObject<Club>(await response.Content.ReadAsStringAsync());
            }

            return club;
        }

        public static async Task<List<CompetitionBoard>> GetCompetitionBoardById(int id)
        {
            var board = new List<CompetitionBoard>();
            var request = RestConnector.COMPETITION_BOARD + "?id=" + id;
            var response = await RestConnector.GetObjectAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                board = JsonConvert.DeserializeObject<List<CompetitionBoard>>(await response.Content.ReadAsStringAsync());
            }

            return board;
        }
    }
}
