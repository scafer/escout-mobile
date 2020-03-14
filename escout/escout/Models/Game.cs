using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Game : BaseModel
    {
        [JsonProperty("timeStart")]
        public string TimeStart { get; set; }
        [JsonProperty("timeEnd")]
        public string TimeEnd { get; set; }
        [JsonProperty("homeColor")]
        public string HomeColor { get; set; }
        [JsonProperty("visitorColor")]
        public string VisitorColor { get; set; }
        [JsonProperty("homeScore")]
        public int HomeScore { get; set; }
        [JsonProperty("visitorScore")]
        public int VisitorScore { get; set; }
        [JsonProperty("homePenaltyScore")]
        public int HomePenaltyScore { get; set; }
        [JsonProperty("visitorPenaltyScore")]
        public int VisitorPenaltyScore { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("homeId")]
        public int HomeId { get; set; }
        [JsonProperty("visitorId")]
        public int VisitorId { get; set; }
        [JsonProperty("competitionId")]
        public int? CompetitionId { get; set; }
        [JsonProperty("imageId")]
        public int? ImageId { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}
