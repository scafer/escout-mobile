using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Athlete : BaseModel
    {
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("fullname")]
        public string fullname { get; set; }
        [JsonProperty("birthDate")]
        public string birthDate { get; set; }
        [JsonProperty("birthPlace")]
        public string birthPlace { get; set; }
        [JsonProperty("citizenship")]
        public string citizenship { get; set; }
        [JsonProperty("height")]
        public double height { get; set; }
        [JsonProperty("weight")]
        public double weight { get; set; }
        [JsonProperty("position")]
        public string position { get; set; }
        [JsonProperty("agent")]
        public string agent { get; set; }
        [JsonProperty("currentInternational")]
        public string currentInternational { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("clubId")]
        public int? clubId { get; set; }
        [JsonProperty("imageId")]
        public int? imageId { get; set; }
        [JsonProperty("created")]
        public string created { get; set; }
        [JsonProperty("updated")]
        public string updated { get; set; }
    }
}
