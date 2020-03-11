﻿using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class GameEvent : BaseModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("gameTime")]
        public string GameTime { get; set; }
        [JsonProperty("eventDescription")]
        public string EventDescription { get; set; }
        [JsonProperty("gameId")]
        public int GameId { get; set; }
        [JsonProperty("eventId")]
        public int EventId { get; set; }
        [JsonProperty("athleteId")]
        public int? AthleteId { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonIgnore]
        public bool Syncronized { get; set; }
    }
}
