using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Game : BaseModel
    {
        [JsonProperty("timeStart")]
        public string timeStart { get; set; }
        [JsonProperty("timeEnd")]
        public string timeEnd { get; set; }
        [JsonProperty("homeColor")]
        public string homeColor { get; set; }
        [JsonProperty("visitorColor")]
        public string visitorColor { get; set; }
        [JsonProperty("homeScore")]
        public int homeScore { get; set; }
        [JsonProperty("visitorScore")]
        public int visitorScore { get; set; }
        [JsonProperty("homePenaltyScore ")]
        public int homePenaltyScore { get; set; }
        [JsonProperty("visitorPenaltyScore")]
        public int visitorPenaltyScore { get; set; }
        [JsonProperty("status")]
        public int status { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("location")]
        public string location { get; set; }
        [JsonProperty("homeId")]
        public int homeId { get; set; }
        [JsonProperty("visitorId")]
        public int visitorId { get; set; }
        [JsonProperty("competitionId")]
        public int? competitionId { get; set; }
        [JsonProperty("imageId")]
        public int? imageId { get; set; }
        [JsonProperty("")]
        public int userId { get; set; }
        [JsonProperty("")]
        public string created { get; set; }
        [JsonProperty("")]
        public string updated { get; set; }
    }
}
