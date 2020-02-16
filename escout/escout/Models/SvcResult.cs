using Newtonsoft.Json;

namespace escout.Models
{
    class SvcResult
    {
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
