using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Event : BaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("sportId")]
        public int SportId { get; set; }
        [JsonProperty("imageId")]
        public int? ImageId { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}
