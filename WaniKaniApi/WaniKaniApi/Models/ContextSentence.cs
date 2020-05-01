using System;
using Newtonsoft.Json;

namespace WaniKaniApi.Models
{
    public class ContextSentence
    {
        [JsonProperty("en")]
        public string En { get; set; }

        [JsonProperty("ja")]
        public string Ja { get; set; }
    }
}
