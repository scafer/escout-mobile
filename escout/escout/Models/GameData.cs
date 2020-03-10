﻿using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models
{
    class GameData
    {
        public Game game { get; set; }
        public Sport sport { get; set; }
        public List<Club> clubs { get; set; }
        public List<Athlete> athletes { get; set; }
        public Competition competition { get; set; }
        public List<Event> events { get; set; }
        public List<GameEvent> gameEvents { get; set; }
    }
}
