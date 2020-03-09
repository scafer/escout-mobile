using escout.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models.GameData
{
    class Game : BaseModel
    {
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public string homeColor { get; set; }
        public string visitorColor { get; set; }
        public int homeScore { get; set; }
        public int visitorScore { get; set; }
        public int homePenaltyScore { get; set; }
        public int visitorPenaltyScore { get; set; }
        public int status { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public int homeId { get; set; }
        public int visitorId { get; set; }
        public int? competitionId { get; set; }
        public int? imageId { get; set; }
        public int userId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }
}
