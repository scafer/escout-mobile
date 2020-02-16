using Newtonsoft.Json;

namespace escout.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("accessLevel")]
        public int AccessLevel { get; set; }
        [JsonProperty("notifications")]
        public int Notifications { get; set; }
        [JsonProperty("imageId")]
        public int ImageId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }

        public User() { }

        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }
    }
}
