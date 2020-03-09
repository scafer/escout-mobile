using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class GameEvent : BaseModel
    {
        [JsonProperty("")]
        public string key { get; set; }
        [JsonProperty("")]
        public string time { get; set; }
        [JsonProperty("")]
        public string gameTime { get; set; }
        [JsonProperty("")]
        public string eventDescription { get; set; }
        [JsonProperty("")]
        public int gameId { get; set; }
        [JsonProperty("")]
        public int eventId { get; set; }
        [JsonProperty("")]
        public int? athleteId { get; set; }
        [JsonProperty("")]
        public int userId { get; set; }
        [JsonProperty("")]
        public string created { get; set; }
        [JsonProperty("")]
        public string updated { get; set; }
        [JsonProperty("")]
        public string syncronized { get; set; }
    }
}
