using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public static class RestConnector
    {
        //API endpoints
        public const string SIGN_IN = "/api/v1/signIn";
        public const string SIGN_UP = "/api/v1/signUp";
        public const string RESET_PASSWORD = "/api/v1/resetPassword";
        public const string CHANGE_PASSWORD = "​/api/v1/changePassword";
        public const string AUTHENTICATED = "/api/v1/authenticated";
        public const string USER = "/api/v1/user";
        public const string USERS = "/api/v1/users";
        public const string ATHLETE = "/api/v1/athlete";
        public const string ATHLETES = "/api/v1/athletes";
        public const string CLUB = "/api/v1/club";
        public const string CLUBS = "/api/v1/clubs";
        public const string COMPETITION = "/api/v1/competition";
        public const string COMPETITIONS = "/api/v1/competitions";
        public const string COMPETITION_BOARD = "/api/v1/competitionBoard";
        public const string EVENT = "/api/v1/event";
        public const string EVENTS = "/api/v1/events";
        public const string GAME = "/api/v1/game";
        public const string GAMES = "/api/v1/games";
        public const string GAME_USER = "/api/v1/gameUser";
        public const string GAME_EVENT = "/api/v1/gameEvent";
        public const string GAME_EVENTS = "/api/v1/gameEvents";
        public const string GAME_ATHLETE = "/api/v1/gameAthlete";
        public const string GAME_ATHLETES = "/api/v1/gameAthletes";
        public const string GAME_DATA = "/api/v1/gameData";
        public const string IMAGE = "/api/v1/image";
        public const string SPORT = "/api/v1/sport";
        public const string SPORTS = "/api/v1/sports";
        public const string FAVORITE = "/api/v1/favorite";
        public const string FAVORITES = "/api/v1/favorites";

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