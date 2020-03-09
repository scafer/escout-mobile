using Newtonsoft.Json;

namespace escout.Models
{
    class CompetitionBoard
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("position")]
        public int position { get; set; }
        [JsonProperty("played")]
        public int played { get; set; }
        [JsonProperty("won")]
        public int won { get; set; }
        [JsonProperty("drawn")]
        public int drawn { get; set; }
        [JsonProperty("lost")]
        public int lost { get; set; }
        [JsonProperty("goalsFor")]
        public int goalsFor { get; set; }
        [JsonProperty("goalsAgainst")]
        public int goalsAgainst { get; set; }
        [JsonProperty("goalsDifference")]
        public int goalsDifference { get; set; }
        [JsonProperty("points")]
        public int points { get; set; }
        [JsonProperty("clubId")]
        public int clubId { get; set; }
        [JsonProperty("competitionId")]
        public int competitionId { get; set; }
        [JsonProperty("created")]
        public string created { get; set; }
        [JsonProperty("updated")]
        public string updated { get; set; }
    }
}
