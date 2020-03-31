using Newtonsoft.Json;

namespace escout.Models.Rest
{
    public class GameAthlete
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("athleteId")]
        public int AthleteId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}