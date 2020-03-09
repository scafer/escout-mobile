using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Event : BaseModel
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("sportId")]
        public int sportId { get; set; }
        [JsonProperty("imageId")]
        public int? imageId { get; set; }
        [JsonProperty("created")]
        public string created { get; set; }
        [JsonProperty("updated")]
        public string updated { get; set; }
    }
}
