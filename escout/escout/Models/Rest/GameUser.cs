using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.Rest
{
    class GameUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("athleteId")]
        public int AthleteId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }

        public GameUser() { }

        public GameUser(int userId, int gameId, int athleteId)
        {
            UserId = userId;
            GameId = gameId;
            AthleteId = athleteId;
        }
    }
}
