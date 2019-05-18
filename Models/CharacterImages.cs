using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class CharacterImages
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("metadata")]
        public string[] Metadata { get; set; }
    }
}
