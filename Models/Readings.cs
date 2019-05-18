using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Readings
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }

        [JsonProperty("accepted_answer")]
        public bool AcceptedAnswer { get; set; }

        [JsonProperty("reading")]
        public string Reading { get; set; }
    }
}
