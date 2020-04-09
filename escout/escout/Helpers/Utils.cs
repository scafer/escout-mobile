using escout.Models;
using escout.Models.Db;
using escout.Models.Rest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public class Utils
    {
        public static DbGame DbGame;

        public static async Task<Image> GetImage(int? imageId)
        {
            var img = new Image();
            var response = await RestConnector.GetObjectAsync(RestConnector.Image + "?id=" + imageId);
            if (!string.IsNullOrEmpty(response))
            {
                img = JsonConvert.DeserializeObject<Image>(response);
            }

            return img;
        }

        public static readonly List<string> AthleteFilter = new List<string>
        {
            {"name"},
            {"age"}
        };

        public static readonly List<string> SoccerEventList = new List<string>
        {
            {string.Empty}, {"Soccer001"}, {"Soccer002"}, {"Soccer003"},
            {"Soccer004"}, {"Soccer005"}, {"Soccer006"}, {"Soccer007"},
            {"Soccer008"}, {"Soccer009"}, {"Soccer010"}, {"Soccer011"},
            {"Soccer012"}, {"Soccer013"}, {"Soccer014"}, {"Soccer015"},
            {"Soccer016"}, {"Soccer017"}, {"Soccer018"}, {"Soccer019"},
            {"Soccer020"}, {"Soccer021"}, {"Soccer022"}, {"Soccer023"},
            {"Soccer024"}, {"Soccer025"}, {"Soccer026"}, {"Soccer027"},
            {"Soccer028"}, {"Soccer029"}, {"Soccer030"},
        };

        public static readonly Dictionary<string, Option> SoccerEvents = new Dictionary<string, Option>
        {
            {SoccerEventList[0], new Option(string.Empty, string.Empty) },
            {SoccerEventList[1], new Option("Ball Recovery", "")},
            {SoccerEventList[2], new Option("Interruption", "") },
            {SoccerEventList[3], new Option("Ball Loss", "") },
            {SoccerEventList[4], new Option("Shot", "") },
            {SoccerEventList[5], new Option("Pass", "") },
            {SoccerEventList[6], new Option("Missed Pass", "") },
            {SoccerEventList[7], new Option("Pass Success", "") },
            {SoccerEventList[8], new Option("Assistance - Yes", "") },
            {SoccerEventList[9], new Option("Assistance - No", "") },
            {SoccerEventList[10], new Option("Out", "") },
            {SoccerEventList[11], new Option("Intercepted", "") },
            {SoccerEventList[12], new Option("On target", "") },
            {SoccerEventList[13], new Option("Ball Stop", "") },
            {SoccerEventList[14], new Option("Kick in Favor", "") },
            {SoccerEventList[15], new Option("Foul Committed", "") },
            {SoccerEventList[16], new Option("Missed Foul", "") },
            {SoccerEventList[17], new Option("Red Card", "") },
            {SoccerEventList[18], new Option("Yellow Card", "") },
            {SoccerEventList[19], new Option("To Post", "") },
            {SoccerEventList[20], new Option("Goalkeeper Defended", "") },
            {SoccerEventList[21], new Option("Goal", "") },
            {SoccerEventList[22], new Option("Penalty", "") },
            {SoccerEventList[23], new Option("Free Kick", "") },
            {SoccerEventList[24], new Option("Missed", "") },
            {SoccerEventList[25], new Option("Defense", "") },
            {SoccerEventList[26], new Option("Goal Kick", "") },
            {SoccerEventList[27], new Option("Ball Possession", "") },
            {SoccerEventList[28], new Option("Opposing Team Ball Possession", "") },
            {SoccerEventList[29], new Option("Grabbed the Ball", "") },
            {SoccerEventList[30], new Option("Didn't Grab the Ball", "") },
        };
    }
}
