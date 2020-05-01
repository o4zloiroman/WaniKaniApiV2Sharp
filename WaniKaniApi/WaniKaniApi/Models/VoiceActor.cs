using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class VoiceActor
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
