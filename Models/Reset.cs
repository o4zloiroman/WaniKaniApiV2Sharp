using Newtonsoft.Json;
using System;

namespace WanikaniApi.Models
{
    public class Reset
    {
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("original_level")]
        public int OriginalLevel { get; set; }

        [JsonProperty("target_level")]
        public int TargetLevel { get; set; }

        [JsonProperty("confirmed_at")]
        public DateTimeOffset ConfirmedAt { get; set; }
    }
}
