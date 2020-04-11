using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public static class RestConnector
    {
        //API endpoints
        public const string SignIn = "/api/v1/signIn";
        public const string SignUp = "/api/v1/signUp";
        public const string ResetPassword = "/api/v1/resetPassword";
        public const string ChangePassword = "​/api/v1/changePassword";
        public const string User = "/api/v1/user";
        public const string Users = "/api/v1/users";
        public const string Athlete = "/api/v1/athlete";
        public const string Athletes = "/api/v1/athletes";
        public const string Club = "/api/v1/club";
        public const string Clubs = "/api/v1/clubs";
        public const string Competition = "/api/v1/competition";
        public const string Competitions = "/api/v1/competitions";
        public const string CompetitionBoard = "/api/v1/competitionBoard";
        public const string Event = "/api/v1/event";
        public const string Events = "/api/v1/events";
        public const string Game = "/api/v1/game";
        public const string Games = "/api/v1/games";
        public const string GameEvent = "/api/v1/gameEvent";
        public const string GameEvents = "/api/v1/gameEvents";
        public const string GameAthlete = "/api/v1/gameAthlete";
        public const string GameAthletes = "/api/v1/gameAthletes";
        public const string GameData = "/api/v1/gameData";
        public const string Image = "/api/v1/image";
        public const string Sport = "/api/v1/sport";
        public const string Sports = "/api/v1/sports";

        public static string Token;
        private const string ApiAuthenticationMode = "Authorization";

        private static string GetApiUrl() => "https://escout-server.herokuapp.com";

        private static string GetAuthenticationHeader() => "bearer" + " " + Token;

        public static async Task<string> GetObjectAsync(string conn)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(ApiAuthenticationMode, GetAuthenticationHeader());
                var httpResponse = await client.GetAsync(GetApiUrl() + conn);
                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                ExceptionHandler.HttpRequestException(ex);
            }
            return response;
        }

        public static async Task<string> PostObjectAsync(string conn, object data)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(ApiAuthenticationMode, GetAuthenticationHeader());

                var httpResponse = await client.PostAsync(GetApiUrl() + conn,
                    new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));

                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                ExceptionHandler.HttpRequestException(ex);
            }
            return response;
        }

        public static async Task<string> PutObjectAsync(string conn, object data)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(ApiAuthenticationMode, GetAuthenticationHeader());

                var httpResponse = await client.PutAsync(GetApiUrl() + conn,
                    new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));

                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                ExceptionHandler.HttpRequestException(ex);
            }
            return response;
        }

        public static async Task<string> DeleteObjectAsync(string conn)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add(ApiAuthenticationMode, GetAuthenticationHeader());

                var httpResponse = await client.DeleteAsync(conn);
                response = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                ExceptionHandler.HttpRequestException(ex);
            }
            return response;
        }
    }
}