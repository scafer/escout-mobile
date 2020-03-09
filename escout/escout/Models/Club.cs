using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Club : BaseModel
    {
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("fullname")]
        public string fullname { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("founded")]
        public string founded { get; set; }
        [JsonProperty("colors")]
        public string colors { get; set; }
        [JsonProperty("members")]
        public string members { get; set; }
        [JsonProperty("stadium")]
        public string stadium { get; set; }
        [JsonProperty("address")]
        public string address { get; set; }
        [JsonProperty("homepage")]
        public string homepage { get; set; }
        [JsonProperty("imageId")]
        public int? imageId { get; set; }
        [JsonProperty("created")]
        public string created { get; set; }
        [JsonProperty("updated")]
        public string updated { get; set; }
    }
}
