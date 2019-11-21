using Newtonsoft.Json;

namespace WanikaniApi.Models
{
    public class AuxiliaryMeaning
    {
        [JsonProperty("meaning")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
