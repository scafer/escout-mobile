using escout.Helpers;
using escout.Models.Rest;

namespace escout.Models.Db
{
    class DbGameEvent : BaseModel
    {
        public string Key { get; set; }
        public string Time { get; set; }
        public string GameTime { get; set; }
        public string EventDescription { get; set; }
        public int GameId { get; set; }
        public int EventId { get; set; }
        public int? AthleteId { get; set; }
        public int UserId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public bool Syncronized { get; set; }

        public DbGameEvent() { }

        public DbGameEvent(GameEvent gameEvent)
        {
            Key = gameEvent.Key;
            Time = gameEvent.Time;
            GameTime = gameEvent.GameTime;
            EventDescription = gameEvent.EventDescription;
            GameId = gameEvent.GameId;
            EventId = gameEvent.EventId;
            AthleteId = gameEvent.AthleteId;
            UserId = gameEvent.UserId;
            Created = gameEvent.Created;
            Updated = gameEvent.Updated;
            Syncronized = false;
        }
    }
}
