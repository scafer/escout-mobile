using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Competition : BaseModel
    {
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("edition")]
        public string edition { get; set; }
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
