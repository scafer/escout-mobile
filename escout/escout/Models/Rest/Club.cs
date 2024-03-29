﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace escout.Models.Rest
{
    public class Club
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("founded")]
        public string Founded { get; set; }

        [JsonProperty("colors")]
        public string Colors { get; set; }

        [JsonProperty("members")]
        public string Members { get; set; }

        [JsonProperty("stadium")]
        public string Stadium { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("imageId")]
        public int? ImageId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("displayOptions")]
        public Dictionary<string, string> DisplayOptions { get; set; }
    }
}
