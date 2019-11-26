using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class CharacterImage
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }
    }
}
