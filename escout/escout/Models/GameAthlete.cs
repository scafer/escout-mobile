using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class GameAthlete : BaseModel
    {
        [JsonProperty("")]
        public int Status { get; set; }
        [JsonProperty("")]
        public int GameId { get; set; }
        [JsonProperty("")]
        public int AthleteId { get; set; }
        [JsonProperty("")]
        public string Created { get; set; }
        [JsonProperty("")]
        public string Updated { get; set; }
    }
}