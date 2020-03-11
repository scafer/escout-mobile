using Newtonsoft.Json;

namespace escout.Models
{
    class Image
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("image")]
        public string Img { get; set; }
        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}
