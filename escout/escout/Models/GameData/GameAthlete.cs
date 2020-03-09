using escout.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.GameData
{
    class GameAthlete : BaseModel
    {
        public int status { get; set; }
        public int gameId { get; set; }
        public int athleteId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }
}