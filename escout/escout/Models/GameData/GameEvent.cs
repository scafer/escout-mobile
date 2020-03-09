using escout.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.GameData
{
    class GameEvent : BaseModel
    {
        public string key { get; set; }
        public string time { get; set; }
        public string gameTime { get; set; }
        public string eventDescription { get; set; }
        public int gameId { get; set; }
        public int eventId { get; set; }
        public int? athleteId { get; set; }
        public int userId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public string syncronized { get; set; }
    }
}
