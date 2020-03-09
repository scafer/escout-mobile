using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class GameAthlete : BaseModel
    {
        [JsonProperty("")]
        public int status { get; set; }
        [JsonProperty("")]
        public int gameId { get; set; }
        [JsonProperty("")]
        public int athleteId { get; set; }
        [JsonProperty("")]
        public string created { get; set; }
        [JsonProperty("")]
        public string updated { get; set; }
    }
}