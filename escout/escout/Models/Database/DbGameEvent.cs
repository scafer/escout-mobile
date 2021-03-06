﻿using escout.Models.Rest;
using escout.Services.Database;

namespace escout.Models.Database
{
    public class DbGameEvent : BaseModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Time { get; set; }
        public string GameTime { get; set; }
        public string EventDescription { get; set; }
        public int GameId { get; set; }
        public int EventId { get; set; }
        public int? AthleteId { get; set; }
        public int? ClubId { get; set; }
        public int UserId { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int DataExt { get; set; }
        public bool Synchronized { get; set; }

        public DbGameEvent() { }

        public DbGameEvent(GameEvent gameEvent)
        {
            DataExt = gameEvent.GameId;
            Key = gameEvent.Key;
            Time = gameEvent.Time;
            GameTime = gameEvent.GameTime;
            EventDescription = gameEvent.EventDescription;
            GameId = gameEvent.GameId;
            EventId = gameEvent.EventId;
            AthleteId = gameEvent.AthleteId;
            ClubId = gameEvent.ClubId;
            UserId = gameEvent.UserId;
            Created = gameEvent.Created;
            Updated = gameEvent.Updated;
            Synchronized = true;
        }
    }
}
