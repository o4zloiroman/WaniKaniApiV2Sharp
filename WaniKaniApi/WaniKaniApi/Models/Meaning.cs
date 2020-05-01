using Newtonsoft.Json;
using System;

namespace WaniKaniApi.Models
{
    public class Meaning
    {
        [JsonProperty("meaning")]
        public string Value { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("accepted_answer")]
        public bool AcceptedAnswer { get; set; }
    }
}
