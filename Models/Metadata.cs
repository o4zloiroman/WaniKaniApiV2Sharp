using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class Metadata
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("source_id")]
        public int SourceId { get; set; }

        [JsonProperty("pronunciation")]
        public string Pronunciation { get; set; }

        [JsonProperty("voice_actor_id")]
        public int VoiceActorId { get; set; }

        [JsonProperty("voice_actor_name")]
        public string VoiceActorName { get; set; }

        [JsonProperty("voice_description")]
        public string VoiceDescription { get; set; }
    }
}
