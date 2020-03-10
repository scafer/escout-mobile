using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace escout.Models
{
    class SearchQuery
    {
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }
        [JsonProperty("condition")]
        public string Condition { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
