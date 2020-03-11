﻿using escout.Helpers;
using Newtonsoft.Json;

namespace escout.Models
{
    public class Competition : BaseModel
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("edition")]
        public string Edition { get; set; }
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
