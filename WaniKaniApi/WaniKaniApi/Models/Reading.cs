using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class Reading
    {
        [JsonProperty("reading")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("accepted_answer")]
        public bool AcceptedAnswer { get; set; }
    }
}
