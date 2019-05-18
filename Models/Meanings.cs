using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Meanings
    {
        [JsonProperty("meaning")]
        public string Meaning { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("accepted_answer")]
        public bool AcceptedAnswer { get; set; }
    }
}
