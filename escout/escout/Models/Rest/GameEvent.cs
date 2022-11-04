using escout.Models.Database;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace escout.Models.Rest
{
    public class GameEvent
    {
        [JsonProperty("id")]
        public int Id { get; set; }

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

        [JsonProperty("clubId")]
        public int? ClubId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("displayOptions")]
        public Dictionary<string, string> DisplayOptions { get; set; }

        public GameEvent() { }

        public GameEvent(DbGameEvent evt)
        {
            Key = evt.Key;
            Time = evt.Time;
            GameTime = evt.GameTime;
            EventDescription = evt.EventDescription;
            GameId = evt.GameId;
            EventId = evt.EventId;
            AthleteId = evt.AthleteId;
            ClubId = evt.ClubId;
            UserId = evt.UserId;
        }
    }
}
