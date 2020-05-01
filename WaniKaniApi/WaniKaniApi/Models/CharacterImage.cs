using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WaniKaniApi.Models
{
    public class CharacterImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("metadata")]
        public JObject Metadata { get; set; }
    }
}
