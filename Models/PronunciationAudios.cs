using System;
using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class PronunciationAudios
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
