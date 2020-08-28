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
            var response = await RestConnector.GetObjectAsync(RestConnector.Image + "?id=" + imageId);
            if (!string.IsNullOrEmpty(response))
            {
                img = JsonConvert.DeserializeObject<Image>(response);
            }

            return img;
        }

        public static async Task<bool> AddGameUser(int gameId, int athleteId)
        {
            try
            {
                List<GameUser> gameUsers = new List<GameUser>();
                gameUsers.Add(new GameUser(int.Parse(App.UserId), gameId, athleteId));
                var response = await RestConnector.PostObjectAsync(RestConnector.GameUser, gameUsers);
                if (!string.IsNullOrEmpty(response))
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
    }
}
