using System.Collections.Generic;
using Newtonsoft.Json;

namespace escout.Models.Rest
{
    public class CompetitionBoard
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("played")]
        public int Played { get; set; }

        [JsonProperty("won")]
        public int Won { get; set; }

        [JsonProperty("drawn")]
        public int Drawn { get; set; }

        [JsonProperty("lost")]
        public int Lost { get; set; }

        [JsonProperty("goalsFor")]
        public int GoalsFor { get; set; }

        [JsonProperty("goalsAgainst")]
        public int GoalsAgainst { get; set; }

        [JsonProperty("goalsDifference")]
        public int GoalsDifference { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("clubId")]
        public int ClubId { get; set; }

        [JsonProperty("competitionId")]
        public int CompetitionId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("displayOptions")]
        public Dictionary<string, string> DisplayOptions { get; set; }
    }
}
