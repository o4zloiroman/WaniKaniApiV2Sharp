using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class AuxiliaryMeanings
    {
        [JsonProperty("meaning")]
        public string Meaning { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
