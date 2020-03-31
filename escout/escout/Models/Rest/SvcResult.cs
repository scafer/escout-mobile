using Newtonsoft.Json;

namespace escout.Models.Rest
{
    class SvcResult
    {
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
