using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace escout.Helpers
{
    class RestConnector
    {
        //Authentication
        public static string Token;
        public const string SignIn = "/api/v1/signIn";
        public const string SignUp = "/api/v1/signUp";

        //User
        public const string ResetPassword = "/api/v1/resetPassword";
        public const string ChangePassword = "​/api/v1/changePassword";
        public const string GetUserInfo = "/api/v1/user";
        public const string GetUsers = "/api/v1/users";

        private const string ApiAuthenticationMode = "Authorization";

        private static string GetApiUrl()
        {
            return "https://escout-server.herokuapp.com";
        }

        private static string GetAuthenticationHeader() => "bearer" + " " + Token;

        public static async Task<string> GetDataFromApi(string conn)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Add(ApiAuthenticationMode, GetAuthenticationHeader());
                response = await client.GetStringAsync(GetApiUrl() + conn);
            }
            catch (HttpRequestException ex)
            {
                ExceptionHandler.HttpRequestException(ex);
            }
            return response;
        }

        public static async Task<string> PostObjectToApi(object data, string conn)
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

        public static async Task<string> PostDataToApi(Dictionary<string, string> values, string conn)
        {
            var response = string.Empty;
            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Add(ApiAuthenticationMode, GetAuthenticationHeader());
                var content = new FormUrlEncodedContent(values);
                var httpResponse = await client.PostAsync(GetApiUrl() + conn, content);
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