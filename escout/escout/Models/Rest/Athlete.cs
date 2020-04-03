using Newtonsoft.Json;

namespace escout.Models.Rest
{
    public class Athlete
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("fullname")]
        public string Fullname { get; set; }
        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }
        [JsonProperty("birthPlace")]
        public string BirthPlace { get; set; }
        [JsonProperty("citizenship")]
        public string Citizenship { get; set; }
        [JsonProperty("height")]
        public double Height { get; set; }
        [JsonProperty("weight")]
        public double Weight { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
        [JsonProperty("positionKey")]
        public int PositionKey { get; set; }
        [JsonProperty("agent")]
        public string Agent { get; set; }
        [JsonProperty("currentInternational")]
        public string CurrentInternational { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("clubId")]
        public int? ClubId { get; set; }
        [JsonProperty("imageId")]
        public int? ImageId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}
